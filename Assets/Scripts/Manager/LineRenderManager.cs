using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderManager : MonoBehaviour
{
    public static LineRenderManager Instance { get; private set; }
    [SerializeField] private Material lineMaterial;
    //line render
    private LineRenderer currentLine;
    private LineRenderer oldLine;
    private Vector3 thePosition;
    
    //the trail when the current line using
    private TrailConnect theTrail;
    private int linePositionAvaiable = 2;

    private bool isCompleteLine = true;

    private void Awake(){
        Instance = this;
    }
    
    private void Start(){
        InputManager.Instance.OnLineNeedRender += OnRequestLineRenderer;
    }

    private void LateUpdate(){
        if (oldLine != null && !isCompleteLine){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.GetMousePosition());
            oldLine.SetPosition(1, mousePosition);
        }
    }

    private void OnRequestLineRenderer(object sender, InputManager.OnLineNeedRenderArgs e){
        //Check the transform is the gameobject need to create line renderer?
        TrailConnect trailConnect = TryGetTrailConnect(e.transformObject);
        if (trailConnect == null ) {
            return;
        }

        //Check if the trail cannot using
        if (trailConnect.GetMainConveyor().gameObject.TryGetComponent(out DeliveryConnect deliveryConnect)){
            if (!deliveryConnect.IsAvailableForUsing()) return;
        }

        //check if oldline has exist and the line is complete
        if (oldLine != null && isCompleteLine){
            LineRenderer newLine = TryGetLineRenderer(trailConnect);
            //Has line
            if (newLine != null) {
                isCompleteLine = false;
                //the Line need to know what is current line, and where is connect to current
                currentLine = oldLine;
            }
            else{
                //The line is new, destroy every old
                //Debug.Log("Clear line");
                Destroy(oldLine);
                theTrail.ClearDeliveryPoint();
                trailConnect.ClearDeliveryPoint();
                oldLine = null;
                theTrail = null;
            
            }
        }

        //Has line exist, start to complete the line
        int position = -1;
        if (oldLine != null && !isCompleteLine){
            //Check the position of the object has same with the previous object
            Vector3 trailPosition = trailConnect.transform.position;
            bool isDifference = trailPosition.x != thePosition.x || trailPosition.z != thePosition.z;

            if (!isDifference) return; //exit this function because has same location

            position = 1;
            isCompleteLine = true;

            //set the trail delivery point
            theTrail.SetDeliveryPoint(trailConnect.GetMainConveyor());
            //Set the receive point with the delivery point
            trailConnect.SetDeliveryPoint(theTrail.GetMainConveyor());
            
        }
        //Setting new line
        else if (oldLine == null && isCompleteLine == true){
            isCompleteLine = false;
            //New line
            currentLine = trailConnect.gameObject.AddComponent<LineRenderer>();
            currentLine.positionCount = linePositionAvaiable;

            position = 0;
            //Save the position of the trail;
            thePosition = trailConnect.transform.position;

            //set the trail = this trail
            theTrail = trailConnect;
            
        }


        //The line will seting the position;
        currentLine.SetPosition(position, trailConnect.transform.position);
        //Set color
        currentLine.startColor = Color.yellow;
        currentLine.endColor = Color.green;
        //Set material
        currentLine.material = lineMaterial;
        oldLine = currentLine;


        //if the line finished, delete the line;
        if (isCompleteLine == true)
            currentLine = null;

        //Continue if has trail to connect
    }

    private TrailConnect TryGetTrailConnect(Transform transform){
        if (transform.TryGetComponent(out TrailConnect trailConnect))
            return trailConnect;
        return null;
    }
    private LineRenderer  TryGetLineRenderer(TrailConnect trailConnect){
        if (trailConnect.TryGetComponent(out LineRenderer lineRenderer))
            return lineRenderer;
        return null;
    }

    //Clear when the bomb bomb;
    public void DestroyCurrentLine(){
        Destroy(oldLine);
        Transform deliveryPoint = theTrail.GetDeliveryPoint();
        //Return if cannot see any delivery point from the trail
        if (deliveryPoint == null) return;

        if (deliveryPoint.TryGetComponent(out DeliveryConnect deliveryConnect)){
            deliveryConnect.ClearDeliveryPoint();
        }
        theTrail.ClearDeliveryPoint();
        oldLine = null;
        theTrail = null;
    }
}
