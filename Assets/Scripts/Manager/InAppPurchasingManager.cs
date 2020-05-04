using EasyMobile;
using UnityEngine;


namespace TrickyRocket.Manager
{
    public class InAppPurchasingManager : MonoBehaviour
    {
        public static InAppPurchasingManager Instance = null;
        private IAPProduct[] m_products;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            GetAllProducts();
        }

        private void OnEnable()
        {
            InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandler;
            InAppPurchasing.PurchaseFailed += PurchaseFailedHandler;
        }

        private void GetAllProducts()
        {
            m_products = InAppPurchasing.GetAllIAPProducts();
        }

        public void BuyProduct(string productName)
        {
            InAppPurchasing.Purchase(productName);
        }

        private void PurchaseCompletedHandler(IAPProduct product)
        {
            // Compare product name to the generated name constants to determine which product was bought
            switch (product.Name)
            {
                case EM_IAPConstants.Product_NoMoreAds:
                    Debug.Log("Product_NoMoreAds was purchased. The user should be granted it now.");
                    break;
                    // More products here...
            }
        }

        private void PurchaseFailedHandler(IAPProduct product)
        {
            Debug.Log("The purchase of product " + product.Name + " has failed.");
        }

        public bool IsNoMoreAdsPurchased()
        {
            return InAppPurchasing.IsProductOwned(EM_IAPConstants.Product_NoMoreAds);
        }
    }
}
