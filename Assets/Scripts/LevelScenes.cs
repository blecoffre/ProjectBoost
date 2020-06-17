using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TrickyRocket
{
    [CreateAssetMenu(fileName = "LevelScenes", menuName = "ScriptableObjects/LevelScenes", order = 1)]
    public class LevelScenes : ScriptableObject
    {
        [SerializeField] private AssetReference[] Levels;

        public string[] GetAllScenesName()
        {
            string[] names = new string[Levels.Length];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = Levels[i].Asset.name;
            }

            return names;
        }
    }
}

