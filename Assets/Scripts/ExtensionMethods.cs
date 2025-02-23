using UnityEngine;
using System.Collections;

//Unity extensions
public static class ExtensionMethods
{

    public static void SetVelocityX(this Rigidbody pRigidbody, float pNewX)
    {
        pRigidbody.linearVelocity = new Vector2(pNewX, pRigidbody.linearVelocity.y);
    }

    public static void SetVelocityY(this Rigidbody pRigidbody, float pNewY)
    {
        pRigidbody.linearVelocity = new Vector2(pRigidbody.linearVelocity.x, pNewY);
    }

    public static void AddVelocityX(this Rigidbody pRigidbody, float pAddX)
    {
        pRigidbody.linearVelocity += new Vector3(pAddX, 0);

    }

    public static void AddVelocityY(this Rigidbody pRigidbody, float pAddY)
    {
        pRigidbody.linearVelocity += new Vector3(0, pAddY);

    }

    public static Vector3 AddVectorX(this Vector3 pVec3, float pAddX)
    {
        return pVec3 += new Vector3(pAddX, 0, 0);

    }

    public static Vector3 AddVectorY(this Vector3 pVec3, float pAddY)
    {
        return pVec3 += new Vector3(0, pAddY, 0);

    }

    public static bool IsInLayerMask(this LayerMask mask, int layer)
    {
        return ((mask.value & (1 << layer)) > 0);
    }

    public static bool IsInLayerMask(this LayerMask mask, GameObject obj)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }

}
