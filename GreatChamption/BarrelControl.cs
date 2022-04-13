
using System.Collections;
namespace GreatChampion
{
   public class BarrelControl:MonoBehaviour
    {
        public bool isexp = false;
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.layer==8)
            {
               if(isexp)
                {
                    GameObject bar = Extension.SpawnObjectSingle(ResourceLoader.explodsion, gameObject.transform.position);
                    bar.SetCustomScalelazy(0.5f);
                    UObject.Destroy(bar, 0.5f);
                }
                
            }
        }
       
       private void OnDestroy()
        {
            isexp = false;
        }
    }
}
