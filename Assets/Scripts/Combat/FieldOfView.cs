using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {


    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public float maskCutawayDst = 0.5f;

    public MeshFilter View;
    public MeshFilter SurroundingView;
    Mesh viewMesh;
    Mesh surroundingMesh;

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        View.mesh = viewMesh;
        surroundingMesh = new Mesh();
        surroundingMesh.name = "Surrounding Mesh";
        SurroundingView.mesh = surroundingMesh;
    }

    void FixedUpdate()
    {
        //FindVisibleTargets();
    }

    void LateUpdate()
    {
        DrawSurroundingView();
        DrawFieldOfView();
        //DrawObsticleMesh();
    }


    /// <summary>
    /// Source https://www.youtube.com/watch?v=rQG9aUWarwE
    /// </summary>
    //void FindVisibleTargets()
    //{
    //    visibleTargets.Clear();
    //    Collider2D [] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
    //    for(int i = 0; i < targetsInViewRadius.Length; i++)
    //    {
    //        Transform target = targetsInViewRadius[i].transform;
    //        Vector3 dirToTarget = (target.position - transform.position);

    //        //Debug.DrawLine(transform.position,transform.position + dirToTarget, Color.magenta);
    //        if (Vector3.Angle(transform.up, dirToTarget.normalized) < viewAngle / 2)
    //        {
    //            float dstToTarget = Vector3.Distance(transform.position, target.position);

    //            RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask);

    //            if (hit.collider == null)
    //            {
    //                visibleTargets.Add(target);
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// DRAWING VIEW
    /// </summary>
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        //for(int i = 1; i <= viewPoints.Count - 1; i++)
        //{
        //    if((viewPoints[i] - viewPoints[i - 1]).normalized != (viewPoints[i + 1] - viewPoints[i]).normalized)
        //    {
        //        Debug.DrawLine(transform.position, viewPoints[i]);
        //    }
        //}

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 1) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]/* + Vector3.up * maskCutawayDst*/);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    /// <summary>
    /// THIS IS NOT WORKING BUT I LEFT IT HERE TO SHOW I TRIED
    /// </summary>
    void DrawObsticleMesh()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.z + viewAngle / 2 - stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 1) * 3];
        vertices[0] = new Vector3(-Mathf.Sin(viewAngle / 2 * Mathf.Deg2Rad), Mathf.Cos(viewAngle / 2 * Mathf.Deg2Rad)) * viewRadius;
        vertices[1] = new Vector3(Mathf.Sin(viewAngle / 2 * Mathf.Deg2Rad), Mathf.Cos(viewAngle / 2 * Mathf.Deg2Rad)) * viewRadius;
        for (int i = 0; i < vertexCount - 2; i++)
        {
            vertices[i + 2] = viewPoints[i]/* + Vector3.up * maskCutawayDst*/;

            if (i < vertexCount)
            {
                triangles[i * 3] = i;
                triangles[i * 3 + 1] = 0;
                triangles[i * 3 + 2] = 1;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    /// <summary>
    /// DRAWING SURROUNDING SPACE
    /// </summary>
    void DrawSurroundingView()
    {
        float iViewAngle = 360 - viewAngle;
        int stepCount = Mathf.RoundToInt(iViewAngle * meshResolution);
        float stepAngleSize = iViewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        //SurroundingCastInfo oldSurroundingCast = new SurroundingCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 - stepAngleSize * i;
            SurroundingCastInfo newSurroundingCast = SurroundingCast(angle);

            viewPoints.Add(newSurroundingCast.point);
            //oldSurroundingCast = newSurroundingCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 1) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]/* + Vector3.up * maskCutawayDst*/);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        surroundingMesh.Clear();
        surroundingMesh.vertices = vertices;
        surroundingMesh.triangles = triangles;
        surroundingMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoints = Vector3.zero;

        for(int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoints = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoints);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, (int)viewRadius, obstacleMask);

        if (hit.collider != null)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    SurroundingCastInfo SurroundingCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, (int)viewRadius, obstacleMask);
        return new SurroundingCastInfo(false, transform.position + dir * viewRadius, viewRadius * 2, globalAngle);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    public struct SurroundingCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public SurroundingCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            {
                pointA = _pointA;
                pointB = _pointB;
            }
        }
    }

}
