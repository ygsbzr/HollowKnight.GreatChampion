
namespace GreatChampion
{
    internal class ChampionControl:MonoBehaviour
    {
        private void Start()
        {
            On.EnemyDreamnailReaction.RecieveDreamImpact += EnemyDreamnailReaction_RecieveDreamImpact;
            PlayMakerFSM ChampionControl = gameObject.LocateMyFSM("FalseyControl");
            if(ChampionControl != null)
            {
                ModifyChampion(ChampionControl);
            }
            GameObject hitter=gameObject.FindGOInChildren("Hitter");
            hitter.AddComponent<HitterControl>();
        }

        private void EnemyDreamnailReaction_RecieveDreamImpact(On.EnemyDreamnailReaction.orig_RecieveDreamImpact orig, EnemyDreamnailReaction self)
        {
            orig(self);
            if(PlayerData.instance.equippedCharm_30)
            {
                HeroController.instance.TakeMP(55);
            }
        }

        private void ModifyChampion(PlayMakerFSM control)
        {
            FsmState openstate = control.GetState("Opened");
            openstate.RemoveTransition("Hit");
            control.GetAction<Wait>("Idle Pause", 2).time = 0.2f;
            control.GetAction<Wait>("R Attack Antic", 3).time = 0.4f;
            control.GetAction<Wait>("Rage", 3).time = 0.149f;
            control.GetState("Land").InsertMethod(3, () =>
            {
                Extension.SpawnObjectCustomDouble(ResourceLoader.origwave, 20f, gameObject.transform.position, 1f);
            });

        }
        private void OnDestroy()
        {
            On.EnemyDreamnailReaction.RecieveDreamImpact -= EnemyDreamnailReaction_RecieveDreamImpact;
        }
    }
}
