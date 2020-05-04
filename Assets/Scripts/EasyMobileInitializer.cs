using UnityEngine;
using EasyMobile;

namespace TrickyRocket
{
    class EasyMobileInitializer : MonoBehaviour
    {
        private void Start()
        {
            if (!RuntimeManager.IsInitialized())
                RuntimeManager.Init();
        }
    }
}
