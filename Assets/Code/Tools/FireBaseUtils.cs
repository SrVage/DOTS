using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using UnityEngine;

namespace Code.Tools
{
    public static class FireBaseUtils
    {
        public static StorageReference storageRef;
        public static StorageReference playerDatsRef;
        public static void CreateReference()
        {
            FirebaseStorage storage = FirebaseStorage.DefaultInstance;
            StorageReference storageRef =
                storage.GetReferenceFromUrl("gs://dots-346314.appspot.com/");
            StorageReference playerDatsRef = storageRef.Child("playerData");
        }

        public static void SaveData(string data, string file)
        {
            var dataByte = Encoding.ASCII.GetBytes(data);
            StorageReference playerData = playerDatsRef.Child(file);
            playerData.PutBytesAsync(dataByte).ContinueWith((Task<StorageMetadata> task) =>
            {
                if (task.IsFaulted || task.IsCanceled) {
                    Debug.Log(task.Exception.ToString());
                    // Uh-oh, an error occurred!
                }
                else {
                    // Metadata contains file metadata such as size, content-type, and download URL.
                    StorageMetadata metadata = task.Result;
                    string md5Hash = metadata.Md5Hash;
                    Debug.Log("Finished uploading...");
                    Debug.Log("md5 hash = " + md5Hash);
                }
            });
        }
    }
}