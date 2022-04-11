
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
                    Extension.SpawnObjectSingle(ResourceLoader.explodsion, gameObject.transform.position).SetCustomScalelazy(0.5f);
                }
                
                   
                
            }
        }
       
       private void OnDestroy()
        {
            isexp = false;
        }
    }
}
