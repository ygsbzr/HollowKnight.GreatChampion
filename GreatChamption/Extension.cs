﻿
namespace GreatChampion
{
	public static class Extension
	{
		public static void  SpawnObjectCustomDouble(GameObject orig,float speed,Vector2 pos,float time)
		{
			float[] Velocities = { -speed, speed };
			Vector3 spawnpos = new(pos.x, pos.y-1.4f, 6.4f);
			Quaternion rotate= Quaternion.identity;
			foreach(float velocity in Velocities)
            {
				float origspeed = velocity;
				if (spawnpos.y > 35f)
					return;
				GameObject cloneobject = UnityEngine.Object.Instantiate(orig, spawnpos, rotate);
				cloneobject.LocateMyFSM("shockwave").GetState("Move").RemoveAction(3);
				cloneobject.LocateMyFSM("shockwave").GetState("Move").RemoveAction(5);
				cloneobject.GetComponent<Rigidbody2D>().velocity = Vector2.right * velocity;
				PlayMakerFSM clonefsm = cloneobject.LocateMyFSM("shockwave");
				clonefsm.GetState("Move").GetAction<FloatAdd>().floatVariable = velocity;
				clonefsm.GetState("Move").GetAction<FloatAdd>().add = origspeed / 2f;
				clonefsm.GetState("Move").GetAction<SetVelocity2d>().x = velocity;
				Vector3 origscale = cloneobject.transform.localScale;
				cloneobject.SetCustomScale(new(origscale.x * 0.5f, origscale.y, origscale.z));
				UObject.Destroy(cloneobject, time);
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
	}
}