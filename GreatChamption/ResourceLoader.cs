using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace GreatChampion
{
   public class ResourceLoader
    {
        public static GameObject explodsion;
        public static GameObject origwave;
        public static GameObject origChampion;
        private static PlayMakerFSM Championfsm;
        public static GameObject BarrelSummon;
        public static GameObject origBarrel;
        public static GameObject origCRshot;
        //Battle Scene/False Knight New/Staff/Staff Head
        public static void LoadResource(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            origChampion = preloadedObjects["GG_Failed_Champion"]["False Knight Dream"];
            origCRshot = preloadedObjects["Mines_04"]["Crystal Flyer"].LocateMyFSM("Crystal Flyer").GetAction<SpawnObjectFromGlobalPool>("Fire", 3).gameObject.Value;
            if (origChampion != null)
            {
                Championfsm = origChampion.LocateMyFSM("FalseyControl");
                origwave = Championfsm.GetAction<SpawnObjectFromGlobalPool>("S Attack Recover", 4).gameObject.Value;
            }
            BarrelSummon = preloadedObjects["GG_Failed_Champion"]["FK Barrel Summon Dream"];
            origBarrel = BarrelSummon.LocateMyFSM("summon").GetAction<SpawnObjectFromGlobalPool>("Spawn", 4).gameObject.Value;
            explodsion = UObject.Instantiate(Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(x => x.name == "Gas Explosion Recycle L"));
            explodsion.transform.parent = null;
            explodsion.SetActive(false);
            UObject.DontDestroyOnLoad(explodsion);
            UObject.Destroy(explodsion.LocateMyFSM("damages_enemy"));
        }
    }
}
