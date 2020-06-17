using System.Collections;
using System.Collections.Generic;
using TrickyRocket.Const;
using TrickyRocket.Manager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TrickyRocket.Controller
{
    public class UpdateCheckerController : MonoBehaviour
    {
        [SerializeField] private UpdateCheckerView m_updateCheckerView = default;
        public string[] LabelsToCheck = { "StoneAgeUnits" };

        private List<string> m_contentToUpdate = new List<string>();
        private float m_downloadSize = 0;
        private float m_currentDownloadedSize = 0;

        public void CheckForContentUpdateAndDownload()
        {
            Addressables.InitializeAsync().Completed += objects =>
            {
                Addressables.ClearDependencyCacheAsync(LabelsToCheck);

                Addressables.CheckForCatalogUpdates().Completed += DoWhenCheckCompleted;
            };
        }

        private void DoWhenCheckCompleted(AsyncOperationHandle<List<string>> handle)
        {
            m_contentToUpdate = handle.Result;

            if (m_contentToUpdate.Count > 0)
            {
                var totalDownloadSizeHandle = GetTotalDownloadSize(m_contentToUpdate);
                totalDownloadSizeHandle.Completed += get =>
                {
                    m_downloadSize = totalDownloadSizeHandle.Result;
                    DownloadFiles(m_contentToUpdate);
                    m_updateCheckerView.ShowView();
                };
            }
            else
            {
                OnDownloadComplete();
            }
        }

        private AsyncOperationHandle<long> GetTotalDownloadSize(List<string> catalogs)
        {
            return Addressables.GetDownloadSizeAsync(catalogs);
        }

        private IEnumerator DownloadFiles(List<string> catalogs)
        {
            foreach (string s in catalogs)
            {
                var handle = Addressables.GetDownloadSizeAsync(catalogs);
                while (!handle.IsDone)
                    yield return new WaitForFixedUpdate();

                float catalogSize = handle.Result;
                float prevPercent = 0;
                var newhandle = Addressables.DownloadDependenciesAsync(catalogs);
                while (!newhandle.IsDone)
                {
                    prevPercent = newhandle.PercentComplete;
                    m_currentDownloadedSize += catalogSize * (newhandle.PercentComplete - prevPercent);

                    m_updateCheckerView.UpdateDownloadInfos(
                        1 / (m_downloadSize / m_currentDownloadedSize),
                        m_currentDownloadedSize, 
                        m_downloadSize
                    );

                    yield return new WaitForFixedUpdate();
                }
            }

            OnDownloadComplete();
        }

        private void OnDownloadComplete()
        {
            m_updateCheckerView.HideView();
            EventManager.TriggerEvent(EventsName.UpdateDone);
        }
    }
}

