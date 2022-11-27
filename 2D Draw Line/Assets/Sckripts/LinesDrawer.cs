using UnityEngine;

public class LinesDrawer : MonoBehaviour
{
    public int count;

    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    private int _cantDrawOverLayerIndex;
 
    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    private Line currentLine;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
        _cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    private void BeginDraw(){
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        count++;

        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    private void Draw(){
        Vector2 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);
       

        if (hit)
            EndDraw();
        else
            currentLine.AddPoint(mousePosition);
    }

    private void EndDraw(){
        if (currentLine != null){
            if(currentLine.pointsCount < 2){
                Destroy(currentLine.gameObject);
            }else{
                currentLine.gameObject.layer = _cantDrawOverLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
}
