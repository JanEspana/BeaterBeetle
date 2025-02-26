using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class DeathMenuManager : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public MenuManager menuManager;
    public TMP_Text roundcount;

    private string apiGetMaxRoundsUrl = "http://localhost:3000/user/maxRounds";
    private string apiUpdateMaxRoundsUrl = "http://localhost:3000/user/updateMaxRounds";

    void Awake()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        PlayerPrefs.DeleteKey("authToken");
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void activateCanvas()
    {
        foreach (GameObject go in gameObjects)
        {
            go.GetComponent<Canvas>().enabled = false;
        }

        gameObject.GetComponent<Canvas>().enabled = true;
        roundcount.text = menuManager.Round.ToString();

        StartCoroutine(CheckAndUpdateMaxRounds());
    }

    IEnumerator CheckAndUpdateMaxRounds()
    {
        string authToken = PlayerPrefs.GetString("authToken", "");

        if (string.IsNullOrEmpty(authToken))
        {
            Debug.Log("No hay usuario logeado.");
            yield break; // Sale de la función si no hay usuario logeado
        }

        using (UnityWebRequest request = UnityWebRequest.Get(apiGetMaxRoundsUrl))
        {
            request.SetRequestHeader("Authorization", "Bearer " + authToken);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;
                UserMaxRoundsResponse response = JsonUtility.FromJson<UserMaxRoundsResponse>(jsonResponse);

                if (menuManager.Round > response.maxRounds)
                {
                    Debug.Log($"Nuevo récord de rondas: {menuManager.Round} (anterior: {response.maxRounds})");
                    StartCoroutine(UpdateMaxRounds(menuManager.Round));
                }
            }
            else
            {
                Debug.LogError("Error al obtener maxRounds: " + request.error);
            }
        }
    }

    IEnumerator UpdateMaxRounds(int newMaxRounds)
    {
        string authToken = PlayerPrefs.GetString("authToken", "");

        if (string.IsNullOrEmpty(authToken))
        {
            Debug.Log("No hay usuario logeado.");
            yield break;
        }

        UserMaxRoundsUpdateData updateData = new UserMaxRoundsUpdateData { maxRounds = newMaxRounds };
        string jsonData = JsonUtility.ToJson(updateData);

        using (UnityWebRequest request = new UnityWebRequest(apiUpdateMaxRoundsUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + authToken);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("maxRounds actualizado en la API.");
            }
            else
            {
                Debug.LogError("Error al actualizar maxRounds: " + request.error);
            }
        }
    }

    [System.Serializable]
    public class UserMaxRoundsResponse
    {
        public int maxRounds;
    }

    [System.Serializable]
    public class UserMaxRoundsUpdateData
    {
        public int maxRounds;
    }
}
