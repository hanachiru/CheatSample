using UnityEngine;
using UnityEngine.UI;

public class Log : MonoBehaviour
{
    private const string Key = "LogCount";
    
    [SerializeField] private Text log;

    private void Start()
    {
        var count = PlayerPrefs.GetInt(Key, 0);
        
        log.text += $"\ndataPath:{Application.dataPath}";
        log.text += $"\npersistentDataPath:{Application.persistentDataPath}";
        log.text += $"\nstreamingAssetsPath:{Application.streamingAssetsPath}";
        log.text += $"\ntemporaryCachePath:{Application.temporaryCachePath}";
        log.text += $"\nLaunch Count:{count}";
        
        count++;
        PlayerPrefs.SetInt(Key, count);
    }
}
