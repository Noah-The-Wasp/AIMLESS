using UnityEngine;
using System.Collections;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;

public class ImpactDecals : MonoBehaviour
{

    IObjectPool<DecalProjector> _impactDecalPool;

    [SerializeField]
    private Material _impactMaterial;

    [SerializeField]
    private LayerMask _impactLayers = -1;

    [SerializeField]
    private Vector3 _impactSize = new Vector3(0.5f, 0.5f, 0.5f);

    [SerializeField]
    private float _impactFade = 5f;

    private void Start()
    {

        _impactDecalPool = new ObjectPool<DecalProjector>(
           createFunc: () =>
           {

               GameObject Projector = new GameObject("DecalProjector");
               DecalProjector Impact = Projector.AddComponent<DecalProjector>();
               Impact.material = _impactMaterial;
               Impact.fadeFactor = 1f;
               Impact.fadeScale = 0.95f;
               Impact.tag = "Decal";
               return Impact;

           },

           actionOnGet: Impact => Impact.gameObject.SetActive(true),
           actionOnRelease: Impact => Impact.gameObject.SetActive(false),
           actionOnDestroy: Impact => Destroy(Impact.gameObject),
           collectionCheck: false,
           defaultCapacity: 10,
           maxSize: 20

        );

    }

    private void OnCollisionEnter(Collision collision)
    {

        SpawnImpact(gameObject.transform.position, gameObject.transform.rotation, collision.transform.position, collision.transform.rotation, collision.contacts[0].normal);

    }

    void SpawnImpact(Vector3 ObjectPosition, Quaternion ObjectRotation, Vector3 CollisionPosition, Quaternion CollisionRotation, Vector3 CollisionNormal)
    {

        DecalProjector Impact = _impactDecalPool.Get();

        Impact.transform.position = ObjectPosition + CollisionNormal * 0.1f;

        Quaternion ImpactRot = Quaternion.LookRotation(-CollisionNormal, Vector3.up);
        Quaternion RandomRot = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        Impact.transform.rotation = ImpactRot * RandomRot;

        Impact.size = _impactSize;

    }

}
