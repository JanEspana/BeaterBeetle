using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class MainMenuManager : MonoBehaviour
{
    private GameObject settingCanvas;
    private UnityEngine.UI.Slider volumeSlider;
    private TMP_InputField usernameInput;
    private TMP_InputField passwordInput;
    public TextMeshProUGUI feedbackText;
    private GameObject registerButtonObj;
    private GameObject loginButtonObj;

    private string apiCheckUrl = "http://localhost:3000/user/all";
    private string apiRegisterUrl = "http://localhost:3000/signup";
    private string apiLoginUrl = "http://localhost:3000/login";

    private string authToken = ""; // Aquí almacenaremos el token JWT

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("username"))
        {
            PlayerPrefs.SetString("username", "Anonymous");
            PlayerPrefs.Save();
        }

        usernameInput = GetInputField("username");
        passwordInput = GetInputField("password");
        feedbackText = GetFeedbackText();

        if (usernameInput != null)
        {
            usernameInput.text = PlayerPrefs.GetString("username");
            usernameInput.onEndEdit.AddListener(delegate { OnUsernameChanged(usernameInput.text); });
        }

        registerButtonObj = GameObject.Find("registerButton");
        if (registerButtonObj != null)
        {
            Button registerButton = registerButtonObj.GetComponent<Button>();
            if (registerButton != null)
            {
                registerButton.onClick.AddListener(CheckUsernameBeforeRegister);
            }
        }

        // **Nuevo: Vincular botón de login**
        loginButtonObj = GameObject.Find("loginButton");
        if (loginButtonObj != null)
        {
            Button loginButton = loginButtonObj.GetComponent<Button>();
            if (loginButton != null)
            {
                loginButton.onClick.AddListener(AttemptLogin);
            }
        }

        volumeSlider = GameObject.FindAnyObjectByType<UnityEngine.UI.Slider>();
        settingCanvas = GameObject.Find("SettingsCanvas");
        if (settingCanvas != null) settingCanvas.SetActive(false);
    }

    private TMP_InputField GetInputField(string fieldName)
    {
        Transform fieldTransform = GameObject.Find(fieldName)?.transform;
        return fieldTransform?.GetComponent<TMP_InputField>();
    }

    private TextMeshProUGUI GetFeedbackText()
    {
        GameObject feedbackObject = GameObject.Find("username/feedbackText");
        return feedbackObject?.GetComponent<TextMeshProUGUI>();
    }

    public void toggleSettings()
    {
        if (settingCanvas != null) settingCanvas.SetActive(!settingCanvas.activeSelf);
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void saveVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void saveUsername()
    {
        if (usernameInput != null)
        {
            PlayerPrefs.SetString("username", usernameInput.text);
            PlayerPrefs.Save();
        }
    }

    public void OnUsernameChanged(string newUsername)
    {
        StartCoroutine(CheckUsernameAvailability(newUsername));
    }

    void CheckUsernameBeforeRegister()
    {
        string usernameToCheck = usernameInput.text;
        StartCoroutine(CheckUsernameAvailability(usernameToCheck, true));
    }

    IEnumerator CheckUsernameAvailability(string username, bool registerIfAvailable = false)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiCheckUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            UserList users = JsonUtility.FromJson<UserList>(jsonResponse);

            bool usernameTaken = false;
            foreach (User user in users.users)
            {
                if (user.username.ToLower() == username.ToLower())
                {
                    usernameTaken = true;
                    break;
                }
            }

            if (feedbackText != null)
            {
                feedbackText.text = usernameTaken ? "Username no disponible" : "Username disponible";
                feedbackText.color = usernameTaken ? Color.red : Color.green;
            }

            if (!usernameTaken && registerIfAvailable)
            {
                StartCoroutine(RegisterNewUser(username));
            }
        }
        else
        {
            if (feedbackText != null)
            {
                feedbackText.text = "Error al conectar con el servidor";
                feedbackText.color = Color.yellow;
            }
        }
    }

    IEnumerator RegisterNewUser(string newUsername)
    {
        string passwordText = passwordInput.text;
        if (string.IsNullOrEmpty(passwordText)) passwordText = "password";

        UserRegisterData userData = new UserRegisterData { username = newUsername, password = passwordText };
        string jsonData = JsonUtility.ToJson(userData);

        using (UnityWebRequest request = new UnityWebRequest(apiRegisterUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Usuario registrado correctamente.");
                feedbackText.text = "Usuario registrado correctamente.";
                feedbackText.color = Color.green;
            }
            else
            {
                Debug.LogError($"Error al registrar: {request.error}");
                feedbackText.text = "Error al registrar.";
                feedbackText.color = Color.red;
            }
        }
    }

    // **Nuevo: Función para iniciar sesión**
    void AttemptLogin()
    {
        StartCoroutine(LoginUser(usernameInput.text, passwordInput.text));
    }

    IEnumerator LoginUser(string username, string password)
    {
        UserRegisterData userData = new UserRegisterData { username = username, password = password };
        string jsonData = JsonUtility.ToJson(userData);

        using (UnityWebRequest request = new UnityWebRequest(apiLoginUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // **Obtener el token de la respuesta JSON**
                string jsonResponse = request.downloadHandler.text;
                AuthResponse response = JsonUtility.FromJson<AuthResponse>(jsonResponse);

                authToken = response.token;
                PlayerPrefs.SetString("authToken", authToken); // Guardar el token en PlayerPrefs
                PlayerPrefs.Save();

                Debug.Log("Inicio de sesión exitoso. Token guardado.");
                feedbackText.text = "Inicio de sesión exitoso.";
                feedbackText.color = Color.green;
            }
            else
            {
                Debug.LogError($"Error al iniciar sesión: {request.error}");
                feedbackText.text = "Error al iniciar sesión.";
                feedbackText.color = Color.red;
            }
        }
    }

    [System.Serializable]
    public class User
    {
        public string username;
    }

    [System.Serializable]
    public class UserList
    {
        public User[] users;
    }

    [System.Serializable]
    public class UserRegisterData
    {
        public string username;
        public string password;
    }

    [System.Serializable]
    public class AuthResponse
    {
        public string token;
    }
}
