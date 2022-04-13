using System.Collections;
namespace GreatChampion
{
    public class HitterControl:MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            GameObject fknight = GameObject.Find("False Knight Dream");
            bool p3 = fknight.LocateMyFSM("FalseyControl").FsmVariables.GetFsmInt("Stunned Amount").Value >= 2;
            if (collider.gameObject.layer == 8)
            {
                
                    if (HeroController.instance.transform.position.x>=fknight.transform.position.x)
                    {
                        Extension.SpawnObjectSingle(ResourceLoader.explodsion, gameObject.transform.position - new Vector3(-5f, 5f, 0f)).SetCustomScalelazy(0.2f);
                    }
                    else
                    {
                        Extension.SpawnObjectSingle(ResourceLoader.explodsion, gameObject.transform.position - new Vector3(5f, 5f, 0f)).SetCustomScalelazy(0.2f);
                    }
                
                GameObject[] barrels=null;
                GameObject[] crshots = null;
                if(!p3)
                {
                    if (fknight.transform.localScale.x > 0)
                    {
                        barrels = Extension.SpawnObjectsShoot(ResourceLoader.origBarrel, new(gameObject.transform.position.x + 5f, URandom.Range(27f, 35f), gameObject.transform.position.z), 1, new(12f, 0f), true);
                        foreach (var barrel in barrels)
                        {
                            var tri = barrel.AddComponent<BarrelControl>();
                            tri.isexp = true;
                        }
                    }
                    else
                    {
                        barrels = Extension.SpawnObjectsShoot(ResourceLoader.origBarrel, new(gameObject.transform.position.x - 5f, URandom.Range(27f, 35f), gameObject.transform.position.z), 1, new(12f, 0f), false);
                        foreach (var barrel in barrels)
                        {
                            var tri = barrel.AddComponent<BarrelControl>();
                            tri.isexp = true;
                        }
                    }
                }
                else
                {
                    GameObject boom = null;
                    if(fknight.transform.localScale.x > 0)
                    {
                       boom = Extension.SpawnObjectSingle(ResourceLoader.origBarrel, gameObject.transform.position - new Vector3(-5f, 4f, 0f));
                    }
                    else
                    {
                        boom = Extension.SpawnObjectSingle(ResourceLoader.origBarrel, gameObject.transform.position - new Vector3(5f, 4f, 0f));
                    }
                    var boomfsm = boom.LocateMyFSM("Fall Barrel Control");
                    boomfsm.GetState("Idle").RemoveAction(3);
                    boomfsm.GetState("Idle").RemoveAction(2);
                    boomfsm.GetState("Idle").RemoveAction(1);
                    boomfsm.GetState("Idle").RemoveAction(0);
                    boomfsm.SetState("Init");
                    boom.GetComponent<Rigidbody2D>().gravityScale = 0;
                    boom.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    if(boom.GetComponent<BoomControl>()==null)
                    {
                        boom.AddComponent<BoomControl>();
                    }
                }
            }
         
        }
    }
}
