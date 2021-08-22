using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerationController : MonoBehaviour, IPooledObject
{
    ObjectsPooling objectPool;

    public void OnObjectSpawn()
    {
        
    }

    void Start()
    {
        objectPool = ObjectsPooling.Instance;
        objectPool.SpawnFromPool("Coletaveis", this.transform.position, Quaternion.identity);
    }



    

}
