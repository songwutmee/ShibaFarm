using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "shibafarm.json");
    
    public static bool LoadOnStart { get; set; }

    public static bool SaveExists() => File.Exists(SavePath);

    public static void DeleteSave()
    {
        if (File.Exists(SavePath)) File.Delete(SavePath);
    }

    public static void SaveGame()
    {
        SaveData data = new SaveData
        {
            money = PlayerWallet.Instance.Balance,
            debt = DebtManager.Instance.remainingDebt,
            day = CalendarSystem.Instance.currentDay
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static SaveData Load()
    {
        if (!SaveExists()) return null;
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}