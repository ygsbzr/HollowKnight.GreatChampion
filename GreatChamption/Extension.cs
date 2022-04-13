
namespace GreatChampion
{
	public static class Extension
	{
		public static void  SpawnObjectCustomDouble(GameObject orig,float speed,Vector2 pos,float time)
		{
			float[] Velocities = { -speed, speed };
			Vector3 spawnpos = new(pos.x, pos.y, 0.008f);
			Quaternion rotate= Quaternion.identity;
			foreach(float velocity in Velocities)
            {
				float origspeed = velocity;
				GameObject cloneobject = UnityEngine.Object.Instantiate(orig, spawnpos, rotate);
				Modding.Logger.LogDebug("Start Wave");
				PlayMakerFSM clonefsm = cloneobject.LocateMyFSM("shockwave");
				clonefsm.SetState("Pause");
				clonefsm.GetState("Move").RemoveAction(3);
				clonefsm.GetState("Move").RemoveAction(5);
				if (velocity>0)
                {
					clonefsm.FsmVariables.FindFsmBool("Facing Right").Value = true;
                }
                else
                {
					clonefsm.FsmVariables.FindFsmBool("Facing Right").Value = false;
				}
				clonefsm.FsmVariables.FindFsmFloat("Speed").Value = speed;
				UObject.Destroy(cloneobject,time);
				/*if(cloneobject.GetComponent<DamageHero>()==null)
                {
					DamageHero damage = cloneobject.AddComponent<DamageHero>();
					damage.damageDealt = 1;
					damage.hazardType = 0;
                }*/


			}


			
		}
		public static GameObject FindGOInChildren(this GameObject parent,string name)
        {
			if(parent != null)
            {
				foreach(Transform child in parent.GetComponentsInChildren<Transform>(true))
                {
					if(child.name == name)
                    {
						return child.gameObject;
                    }
                }
            }
			return null;
        }
		public static void SetCustomScale(this GameObject go, Vector3 Scale)
        {
			if(go != null)
            {
				go.transform.localScale = Scale;
            }
        }
		public static void SetCustomScalelazy(this GameObject go, float pp)
		{
			if (go != null)
			{
				go.transform.localScale *=pp ;
			}
		}
		public static GameObject SpawnObjectSingle(GameObject orig,Vector3 pos)
        {
			GameObject cloneobject = UObject.Instantiate(orig, pos,Quaternion.identity);
			cloneobject.SetActive(true);
			return cloneobject;
        }
		public static GameObject[] SpawnObjectsShoot(GameObject orig,Vector3 pos,int amount,Vector2 origv,bool isright)
        {
			List<GameObject> list = new List<GameObject>();
			for(int i = 0; i < amount; i++)
            {
				if(Count%2==0)
                {
					GameObject shoot = UObject.Instantiate(orig, pos + new Vector3(0f, 5f * i, 0f), Quaternion.identity);
					var rigid = shoot.GetComponent<Rigidbody2D>();
					rigid.gravityScale = URandom.Range(0f, 0.5f);
					rigid.velocity = new((origv.x + 2 * i) * (isright ? 1 : -1), origv.y + 3 * i); ;
					list.Add(shoot);
					
				}
            }
			Count++;
			return list.ToArray();
        }
		public static int Count = 0;
	}
	
}