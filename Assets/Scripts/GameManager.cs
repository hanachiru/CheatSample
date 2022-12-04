using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    
    private string _saveDataPath;
    private Score _score;
    
    private void Start()
    {
        _saveDataPath = $"{Application.persistentDataPath}/SaveData.json";
        _score = LoadScore(_saveDataPath) ?? new Score();
        scoreText.text = _score.value.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _score.value++;
            scoreText.text = _score.value.ToString();
        }
    }

    private void OnDestroy()
    {
        SaveScore(_saveDataPath, _score);
    }

    private static Score LoadScore(string path)
    {
        if (!File.Exists(path)) return default;
        
        var hp = File.ReadAllText(path);
        return JsonUtility.FromJson<Score>(hp);
    }

    private static void SaveScore(string path, Score value)
    {
        var json = JsonUtility.ToJson(value);
        File.WriteAllText(path, json);
    }
}

[Serializable]
public class Score
{
    public int value;
}