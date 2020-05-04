using EasyMobile;
using TrickyRocket.Manager;
using UnityEngine;

namespace TrickyRocket.View
{
    class NoAdsPurchaseView : MonoBehaviour
    {
        private void Start()
        {
            HideView();
        }

        private void OnDisable()
        {
            if (!InAppPurchasingManager.Instance.IsNoMoreAdsPurchased())
                InAppPurchasing.PurchaseCompleted -= HideView;
        }

        /// <summary>
        /// Hide the purchase option if player already has it
        /// </summary>
        private void HideView(IAPProduct product = null)
        {
            if (InAppPurchasingManager.Instance.IsNoMoreAdsPurchased())
            {
                gameObject.SetActive(false);
            }
            else
            {
                InAppPurchasing.PurchaseCompleted += HideView;
            }
        }

        public void BuyNoAds()
        {
            InAppPurchasingManager.Instance.BuyProduct(EM_IAPConstants.Product_NoMoreAds);
        }
    }
}
