
namespace GreatChampion
{
    public class HitterControl:MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.layer == 8)
            {
               if(gameObject.transform.position.x<HeroController.instance.transform.position.x)
                {
                    Extension.SpawnObjectSingle(ResourceLoader.explodsion, gameObject.transform.position - new Vector3(-5f, 3f, 0f)).SetCustomScalelazy(0.5f);
                }
                else
                {
                    Extension.SpawnObjectSingle(ResourceLoader.explodsion, gameObject.transform.position - new Vector3(5f, 3f, 0f)).SetCustomScalelazy(0.5f);
                }
            }
        }
    }
}
