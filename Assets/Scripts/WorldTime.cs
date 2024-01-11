using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;

public class WorldTime : MonoBehaviour
{
    #region Singleton

    private static WorldTime _instance;

    public static WorldTime Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WorldTime>();
            }

            return _instance;
        }
    }

    #endregion

    [SerializeField] private string URL = "https://script.googleusercontent.com/macros/echo?user_content_key=29aVcCJfWkxMZmVxrGgCotKt9G-5NljaJi2Ejncpd48n4t4RbYjmpXb3w19pg0-3Smt-HDrBJRNKT6V-3FDaf0mx6Sk1SSWFm5_BxDlH2jW0nuo2oDemN9CCS2h10ox_1xSncGQajx_ryfhECjZEnJ9GRkcRevgjTvo8Dc32iw_BLJPcPfRdVKhJT5HNzQuXEeN3QFwl2n0M6ZmO-h7C6bwVq0tbM60-YSRgvERRRx91eQMV9hTntRGQmSuaYtHQ&lib=MwxUjRcLr2qLlnVOLh12wSNkqcO1Ikdrk";

    public event Action<TimeData> OnTimeChanged;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public async void GetTime()
    {
        var timeData = await GetRealTimeFromAPI();

        OnTimeChanged.Invoke(timeData);

        await UniTask.Delay(TimeSpan.FromHours(1));
        //await UniTask.Delay(TimeSpan.FromSeconds(5));
        GetTime();
    }

    public void UpdateTime(TimeData timeData)
    {
        OnTimeChanged.Invoke(timeData);
    }

    private async UniTask<TimeData> GetRealTimeFromAPI()
    {
        var webRequest = await UnityWebRequest
            .Get(URL)
            .SendWebRequest();


        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log($"Error: {webRequest.downloadHandler.error}");
        }

        var timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);
        return timeData;
    }
}
