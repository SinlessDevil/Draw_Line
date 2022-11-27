using UnityEngine;
using System.Collections.Generic;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2D;
    public Rigidbody2D rigidbody2D;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    private float _pointsMinDistance = 0.1f;
    private float _circleColliderRadius;

    public void AddPoint(Vector2 newpoint){
        if (pointsCount >= 1 && Vector2.Distance(newpoint, GetLastPoint()) < _pointsMinDistance)
            return;

        points.Add(newpoint);
        pointsCount++;

        //Add Circle Collder to the Point
        CircleCollider2D circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newpoint;
        circleCollider.radius = _circleColliderRadius;


        //Line Renderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newpoint);

        //Edge Collider
        if (pointsCount > 1)
            edgeCollider2D.points = points.ToArray();
    }

    public Vector2 GetLastPoint(){
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }

    public void UsePhysics(bool usePhysics){
        rigidbody2D.isKinematic = !usePhysics;
    }

    public void SetLineColor(Gradient colorGradient){
        lineRenderer.colorGradient = colorGradient;
    }

    public void SetPointsMinDistance(float distance){
        _pointsMinDistance = distance;
    }

    public void SetLineWidth(float width){
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        _circleColliderRadius = width / 2f;
        edgeCollider2D.edgeRadius = _circleColliderRadius;
    }
}
