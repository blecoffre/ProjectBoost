using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TrickyRocket
{
    public class UpdateCheckerView : MonoBehaviour
    {
        [SerializeField] private Image m_progressBar = default;
        [SerializeField] private TextMeshProUGUI m_progressText = default;

        public void UpdateDownloadInfos(float progressPercent, float currentSizeDownloaded, float totalSizeToDownload)
        {
            UpdateProgressBar(progressPercent);
            UpdateProgressText(currentSizeDownloaded, totalSizeToDownload);
        }

        private void UpdateProgressBar(float percent)
        {
                if (m_progressBar)
            m_progressBar.fillAmount = percent;
        }

        private void UpdateProgressText(float current, float total)
        {
            m_progressText?.SetText(FormatProgressText(current, total));
        }

        private string FormatProgressText(float current, float total)
        {
            return string.Format("{0}mo / {1}mo", current, total);
        }

        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}

