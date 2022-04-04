
using System.Collections;
namespace GreatChampion
{
    internal class BarrelControl:MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.layer==8)
            {
                int choose = URandom.Range(0, 50);
                if(choose<=24)
                {
                    if(choose<=12)
                    {
                        StartCoroutine(CreateDoubleWave());
                    }
                    else
                    {
                        Extension.SpawnObjectSingle(ResourceLoader.explodsion, gameObject.transform.position).SetCustomScalelazy(0.5f);
                    }
                }
            }
        }
        public  IEnumerator CreateDoubleWave()
        {
            Extension.SpawnObjectCustomDouble(ResourceLoader.origwave, 20f, gameObject.transform.position,1f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
