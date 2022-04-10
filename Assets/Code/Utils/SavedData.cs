using System.IO;
using UnityEngine;

namespace Code.Utils
{
    public static class SavedData
    {
        private const string FileName = "/playerData.bin";

        public static void Save(PlayerData data)
        {
            string stringData = JsonUtility.ToJson(data);
            File.WriteAllText(Application.dataPath+FileName, stringData);
        }

        public static PlayerData Load()
        {
            var stringData = File.ReadAllText(Application.dataPath + FileName);
            return JsonUtility.FromJson<PlayerData>(stringData);
        }
    }
}