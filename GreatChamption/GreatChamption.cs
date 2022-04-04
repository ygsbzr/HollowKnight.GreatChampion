using System.Collections;
namespace GreatChampion
{
    public class GreatChampion : Mod,ITogglableMod
    {
        public override string GetVersion() => "1.0";
        public override List<(string, string)> GetPreloadNames()
        {
            return new()
            {
                ("GG_Failed_Champion", "False Knight Dream"),
            };
        }
        public GreatChampion instance;
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            ResourceLoader.LoadResource(preloadedObjects);
            instance = this;
            USceneManager.activeSceneChanged += CheckScene;
            ModHooks.LanguageGetHook += ChangeText;
        }

        private string ChangeText(string key, string sheetTitle, string orig)
        {
           if(key== "NAME_FAILED_CHAMPION")
            {
                return "GreatChampion";
            }
           if(key== "GG_S_FAILED_CHAMPION")
            {
                return "Fierce God of Explosion";
            }
           if(key== "FALSE_KNIGHT_DREAM_MAIN")
            {
                return "GreatChampion";
            }
            return orig;
        }

        private void CheckScene(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            if(arg1.name== "GG_Failed_Champion")
            {
                Log("Enter Champion scene");
                GameManager.instance.StartCoroutine(CheckChampion());
                ModHooks.ObjectPoolSpawnHook += ChangeBarrel;
            }
            else
            {
                ModHooks.ObjectPoolSpawnHook -= ChangeBarrel;
            }
        }

        private GameObject ChangeBarrel(GameObject arg)
        {
            if(arg.name.StartsWith("Falling Barrel"))
            {
                BarrelControl barrelControl=arg.GetComponent<BarrelControl>();
                if(barrelControl==null)
                {
                    arg.AddComponent<BarrelControl>();
                }
            }
            return arg;
        }
        private IEnumerator CheckChampion()
        {
            yield return new WaitWhile(() => GameObject.Find("False Knight Dream") == null);
            GameObject.Find("False Knight Dream").AddComponent<ChampionControl>();
        }
        public void Unload()
        {
            USceneManager.activeSceneChanged -= CheckScene;
            ModHooks.LanguageGetHook -= ChangeText;
        }
    }
}
