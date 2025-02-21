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
    private GameObject username;
    private GameObject password;
    public TextMeshProUGUI feedbackText;
    private GameObject registerButtonObj;
    private TMP_InputField usernameInput;
    private TMP_InputField passwordInput;

    private string apiCheckUrl = "http://localhost:3000/users/all";  // URL para verificar username
    private string apiRegisterUrl = "http://localhost:3000/signup"; // URL para registrar usuario

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("username"))
        {
            PlayerPrefs.SetString("username", "Anonymous");
            PlayerPrefs.Save();
        }

        // Obtener referencias de los campos de usuario y contraseña
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
        StartCoroutine(CheckUsernameAvailability(usernameToCheck, true)); // Ahora también registra si está disponible
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

            // Si el username está disponible y se activó la bandera, registrarlo
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
        if (string.IsNullOrEmpty(passwordText))
        {
            passwordText = "password"; // Si está vacío, usar "password" por defecto
        }

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
                if (feedbackText != null)
                {
                    feedbackText.text = "Usuario registrado correctamente.";
                    feedbackText.color = Color.green;
                }
            }
            else
            {
                Debug.LogError($"Error al registrar: {request.error}");
                if (feedbackText != null)
                {
                    feedbackText.text = "Error al registrar.";
                    feedbackText.color = Color.red;
                }
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
}
