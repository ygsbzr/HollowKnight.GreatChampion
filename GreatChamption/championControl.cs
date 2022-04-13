using System.Collections;
namespace GreatChampion
{
    internal class ChampionControl:MonoBehaviour
    {
        private SRandom _rand=new SRandom();
        private void Start()
        {
            On.EnemyDreamnailReaction.RecieveDreamImpact += EnemyDreamnailReaction_RecieveDreamImpact;
            PlayMakerFSM ChampionControl = gameObject.LocateMyFSM("FalseyControl");
            PlayMakerFSM summoncontrol = GameObject.Find("FK Barrel Summon Dream").LocateMyFSM("summon");
            if(ChampionControl != null)
            {
                ModifyChampion(ChampionControl);
            }
            if(summoncontrol != null)
            {
                ModifySummon(summoncontrol);
            }
            hitter = gameObject.FindGOInChildren("Hitter");
            hitter.AddComponent<HitterControl>();
            _anim = gameObject.GetComponent<tk2dSpriteAnimator>();
            _Hitanim=hitter.GetComponent<tk2dSpriteAnimator>();
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
            control.GetAction<Wait>("Idle Pause", 2).time = 0f;
            control.GetAction<Wait>("R Attack Antic", 3).time = 0.4f;
            control.GetAction<Wait>("Rage", 3).time = 0.149f;
            control.GetAction<FloatCompare>("Move Choice", 2).float2 = 28f;
            control.GetAction<SetVelocity2d>("JA Jump", 4).y = 65f;
            control.GetState("State 1").InsertMethod(0, () =>
            {
                _anim.GetClipByName("Land").fps = 30;
            });
            control.GetState("JA Antic").InsertMethod(0,() =>
            {
                int choose = _rand.Next(0, 15);
                if(choose<5)
                {
                    _anim.GetClipByName("Jump Antic").fps = 20;
                    _anim.GetClipByName("Jump Attack Up").fps = 25;
                }
                else if(5<choose&&choose<10)
                {
                    _anim.GetClipByName("Jump Antic").fps = 25;
                    _anim.GetClipByName("Jump Attack Up").fps = 20;
                }
                else
                {
                    _anim.GetClipByName("Jump Antic").fps = 25;
                    _anim.GetClipByName("Jump Attack Up").fps = 30;
                }
            });
            control.GetState("JA Hit").InsertMethod(0, () =>
            {
                int choose = _rand.Next(0, 15);
                if (choose < 5)
                {
                   
                    _anim.GetClipByName("Jump Attack Hit 1").fps = 20;
                }
                else if (5 < choose && choose < 10)
                {
                    
                    _anim.GetClipByName("Jump Attack Hit 1").fps = 25;
                }
                else
                {
                    
                    _anim.GetClipByName("Jump Attack Hit 1").fps = 30;
                }
            });
            control.GetState("JA Recoil").InsertMethod(0, () =>
            {
                int choose = _rand.Next(0, 15);
                if (choose < 5)
                {

                    _anim.GetClipByName("Jump Attack Hit 2").fps = 20;
                }
                else if (5 < choose && choose < 10)
                {

                    _anim.GetClipByName("Jump Attack Hit 2").fps = 25;
                }
                else
                {

                    _anim.GetClipByName("Jump Attack Hit 2").fps = 30;
                }
            });
            control.GetState("JA Recoil 2").InsertMethod(0, () =>
            {
                int choose = _rand.Next(0, 15);
                if (choose < 5)
                {

                    _anim.GetClipByName("Jump Attack Hit 3").fps = 20;
                }
                else if (5 < choose && choose < 10)
                {

                    _anim.GetClipByName("Jump Attack Hit 3").fps = 25;
                }
                else
                {

                    _anim.GetClipByName("Jump Attack Hit 3").fps = 30;
                }
            });
            control.GetState("Jump").InsertMethod(0, () =>
            {
                int choose = _rand.Next(0, 15);
                if (choose < 5)
                {
                    _anim.GetClipByName("Jump").fps = 20;
                }
                else if (5 < choose && choose < 10)
                {
                    _anim.GetClipByName("Jump").fps = 25;
                }
                else
                {
                    _anim.GetClipByName("Jump").fps = 30;
                }
            });
            control.GetState("Land Noise").InsertMethod(0,()=>
            {
                Extension.SpawnObjectCustomDouble(ResourceLoader.origwave, 20f, new( gameObject.transform.position.x,26f), 3);
                Modding.Logger.LogDebug("Spawn Wave");
            });
            FsmState rollstate = control.CreateState("Roll");
            control.GetState("Move Choice").AddTransition("START ROLL", "Roll");
            rollstate.AddTransition("ROLLING", "Move Choice");
            FsmState ragerollstate = control.CreateState("Rage Roll");
            ragerollstate.AddTransition("ROLL CENTER", "State 2");
            control.ChangeTransition("Recover", "FINISHED", "Rage Roll");
            ragerollstate.AddMethod(() =>
            {
                _Hitanim.Play("Blank");
                hitter.SetActive(false);
            });
            ragerollstate.AddMethod(() =>
            {
                _anim.Play("Stun Roll");
                Vector2 vector = new(20f * ((gameObject.transform.localScale.x < 0) ? -1 : 1), 0f);
                gameObject.GetComponent<Rigidbody2D>().velocity = vector;
                StartCoroutine(RollToCenter());
            });
            rollstate.AddMethod(() =>
            {
                _Hitanim.Play("Blank");
                hitter.SetActive(false);
            });
            rollstate.AddMethod(() =>
            {
                _anim.Play("Stun Roll");
                Vector2 vector = new(20f * ((gameObject.transform.localScale.x < 0) ? -1 : 1), 0f);
                gameObject.GetComponent<Rigidbody2D>().velocity = vector;
                gameObject.GetComponent<HealthManager>().InvincibleFromDirection = 0;
                gameObject.GetComponent<HealthManager>().IsInvincible = true;
                StartCoroutine(RollToHero());
            });
            control.GetAction<SendRandomEvent>("Move Choice", 4).events = (from x in control.GetState("Move Choice").Transitions
                                                                           select x.FsmEvent into x
                                                                           orderby x.Name
                                                                           select x).ToArray<FsmEvent>();
            control.GetAction<SendRandomEvent>("Move Choice", 4).weights = new FsmFloat[6] { 1f, 1f, 1f, 1f,1f,1f };
            control.GetAction<SetScale>("S Attack Recover", 5).x = 1.25f;
            control.FsmVariables.GetFsmInt("Jump Barrel Min").Value = 0;
            control.FsmVariables.GetFsmInt("Jump Barrel Max").Value = 0;
            control.FsmVariables.GetFsmInt("Slam Barrel Min").Value = 0;
            control.FsmVariables.GetFsmInt("Slam Barrel Max").Value = 0;
            gameObject.GetComponent<HealthManager>().hp = 800;
            control.FsmVariables.FindFsmInt("Recover HP").Value = 800;
            gameObject.RefreshHPBar();
        }
        public IEnumerator RollToHero()
        {
            bool islft=gameObject.transform.localScale.x < 0;
            float herox = HeroController.instance.transform.position.x;
            yield return new WaitWhile(() =>
               (gameObject.transform.position.x <= herox - 2.5 && !islft)|| (gameObject.transform.position.x >= herox + 2.5 && islft)
            );
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _anim.StopAndResetFrame();
            gameObject.GetComponent<HealthManager>().InvincibleFromDirection = 0;
            gameObject.GetComponent<HealthManager>().IsInvincible = false;
            gameObject.LocateMyFSM("FalseyControl").SendEvent("ROLLING");
        }
        public IEnumerator RollToCenter()
        {
            bool islft = gameObject.transform.localScale.x < 0;
            yield return new WaitWhile(() =>
                (gameObject.transform.position.x <= 60f && !islft) || (gameObject.transform.position.x >= 60f && islft)
            );
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _anim.StopAndResetFrame();
            gameObject.GetComponent<HealthManager>().IsInvincible = false;
            gameObject.GetComponent<HealthManager>().InvincibleFromDirection = 0;
            gameObject.LocateMyFSM("FalseyControl").SendEvent("ROLL CENTER");
        }
        private void OnDestroy()
        {
            On.EnemyDreamnailReaction.RecieveDreamImpact -= EnemyDreamnailReaction_RecieveDreamImpact;
        }
        public IEnumerator CreateDoubleWave()
        {
            Extension.SpawnObjectCustomDouble(ResourceLoader.origwave, 20f, gameObject.transform.position + new Vector3(5f, 3f, 0f), 1f);
            yield return new WaitForSeconds(0.5f);
        }
        private void ModifySummon(PlayMakerFSM control)
        {
           
        }
        tk2dSpriteAnimator _anim;
        GameObject hitter;
        tk2dSpriteAnimator _Hitanim;
    }
}
