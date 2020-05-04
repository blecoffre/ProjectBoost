#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("VNFqBKPatCKtUHz5g621yD45Z+HVFbs/637CFoUA79MJtksMSnuvX5hNm3MDS0NokcnViqNXcgHkkx5cP8pe92BLuQD6JwEBEhScZ5n0FJiwhseI6zOZ4pTtIccXeQwDyPbmAd9TYRu15lctfaCXnT6/5bF07JVX+OCLKFpZqUMpZz1uv6zNU+M5dbsvbFtQ/hrSIITUeTkczy9EMNIj9y3LqbbWPNAEsnj2evNOoIMlOxTWAJQ/8k60zBPVqYTGcGUnUaRec/9VfYc3TYNAbkACvzIWaABZfLimKr8Njq2/gomGpQnHCXiCjo6Oio+Mz8gK0JK3tWb9zq5e/JrdJ/cRX44NjoCPvw2OhY0Njo6PDSafj0SUTqwAoFicnJi+qI2Mjo+O");
        private static int[] order = new int[] { 12,6,8,5,8,11,8,13,9,13,10,12,12,13,14 };
        private static int key = 143;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
