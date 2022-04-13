
namespace GreatChampion
{
    internal class BoomControl:MonoBehaviour
    {
        private float time = 0f;
        public float BoomTime=2f;
        private void Start()
        {
            time = 0f;
        }
        private void Update()
        {
            time+=Time.deltaTime;
            if(time>=BoomTime)
            {
                GameObject bar= Extension.SpawnObjectSingle(ResourceLoader.explodsion, gameObject.transform.position);
                bar.SetCustomScalelazy(0.2f);
                Destroy(bar, 0.5f);
                Destroy(gameObject);
            }
        }
        private void OnDestroy()
        {
            time = 0f;
        }
    }
}
