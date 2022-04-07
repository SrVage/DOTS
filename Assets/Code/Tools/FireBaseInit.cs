using Firebase;
using UnityEngine;

namespace Code.Tools
{
    public class FireBaseInit:MonoBehaviour
    {
        private void Start()
        {
            FirebaseApp.FixDependenciesAsync().ContinueWith(task =>
            {
                FireBaseUtils.CreateReference();
            });
        }
    }
}