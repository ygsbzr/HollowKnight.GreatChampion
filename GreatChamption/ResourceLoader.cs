
namespace GreatChampion
{
   public class ResourceLoader
    {
        public static GameObject explodsion;
        public static GameObject origwave;
        public static GameObject origChampion;
        private static PlayMakerFSM Championfsm;
        public static void LoadResource(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            origChampion = preloadedObjects["GG_Failed_Champion"]["False Knight Dream"];
            if (origChampion != null)
            {
                Championfsm = origChampion.LocateMyFSM("FalseyControl");
                origwave = Championfsm.GetAction<SpawnObjectFromGlobalPool>("S Attack Recover", 4).gameObject.Value;
            }
            explodsion = UObject.Instantiate(Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(x => x.name == "Gas Explosion Recycle L"));
            explodsion.transform.parent = null;
            explodsion.SetActive(false);
            UObject.DontDestroyOnLoad(explodsion);
            UObject.Destroy(explodsion.LocateMyFSM("damages_enemy"));
        }
    }
}
