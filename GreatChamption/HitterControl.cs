
namespace GreatChampion
{
    public class HitterControl:MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            GameObject fknight = GameObject.Find("False Knight Dream");
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
                if(fknight.transform.localScale.x>0)
                {
                   barrels = Extension.SpawnObjectsShoot(ResourceLoader.origBarrel, new(gameObject.transform.position.x+5f,URandom.Range(27f,35f),gameObject.transform.position.z), 1, new(12f, 0f), true);
                    foreach(var barrel in barrels)
                    {
                        var tri = barrel.AddComponent<BarrelControl>();
                        tri.isexp = true;
                    }
                }
                else
                {
                    barrels = Extension.SpawnObjectsShoot(ResourceLoader.origBarrel, new(gameObject.transform.position.x-5f, URandom.Range(27f, 35f), gameObject.transform.position.z), 1, new(12f, 0f), false);
                    foreach (var barrel in barrels)
                    {
                        var tri = barrel.AddComponent<BarrelControl>();
                        tri.isexp = true;
                    }
                }
            }
        }
    }
}
