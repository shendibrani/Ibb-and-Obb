using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class RaycastController : MonoBehaviour
{
    public LayerMask CollisionMask;

    public float RaycastOffsetMargins = .015f;
    public int VerticalRayCount = 4;
    public float RayLength;
    
    [HideInInspector]
    public float VerticalRaySpacing;

    [HideInInspector]
    public BoxCollider Collider;
    public RaycastOrigins RayCastLocations;
    public CollisionInfo Collisions;


    private void Start()
    {
        Collider = GetComponent<BoxCollider>();
        CalculateRaySpacing();
    }

    private void Update()
    {
        UpdateRaycastOrigins();
        Collisions.Reset();
        
        RaycastRelativeBottom();
    }

    /// <summary>
    /// Sets Ray locations on the body with even spacing.
    /// </summary>
    public void UpdateRaycastOrigins()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(RaycastOffsetMargins * -2);

        RayCastLocations.bottom = new Vector2(bounds.min.x, bounds.min.y);
        RayCastLocations.top = new Vector2(bounds.min.x, bounds.max.y);
    }

    /// <summary>
    /// Loops through all rays to output CollisionInfo relative to player's bottom.
    /// </summary>
    /// <param name="upsideDown"></param>
    private void RaycastRelativeBottom()
    {
        Collisions.RayHit = new RaycastHit();
        for (int i = 0; i < VerticalRayCount; i++)
        {
            Vector2 rayOrigin = transform.up.y > 0 ? RayCastLocations.bottom : RayCastLocations.top;
            rayOrigin += Vector2.right * (VerticalRaySpacing * i);

            if (Collisions.RayHit.transform == null)
                Collisions.Below = Physics.Raycast(rayOrigin, transform.up * -1, out Collisions.RayHit, RayLength,
                    CollisionMask);

            Debug.DrawRay(rayOrigin, transform.up * -1 * RayLength, Color.red);
        }
    }
    
    /// <summary>
    /// Calculates the size in between rays depending on the bounds size.
    /// </summary>
    public void CalculateRaySpacing()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(RaycastOffsetMargins * -2);

        VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);
        
        VerticalRaySpacing = bounds.size.x / (VerticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 top;
        public Vector2 bottom;
    }

    public struct CollisionInfo
    {
        public bool Below;

        public RaycastHit RayHit;
      
        public LayerMask HitLayer
        {
            get { return RayHit.transform == null? 0: RayHit.transform.gameObject.layer; }
        }

        public void Reset()
        {
            Below = false;
        }
    }
}