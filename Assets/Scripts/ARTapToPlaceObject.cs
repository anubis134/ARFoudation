using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR

using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.UI;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    //private ARSessionOrigin arOrigin;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    public List<GameObject> Images = new List<GameObject>();
    public SelectCategory selectCategory; 
    // images for rotation buffered objects
    public GameObject RotationImage;
    public GameObject DestroyImage;
    [HideInInspector]
    public GameObject bufferedObject;
    private float BufferedObjectSize;
    public Scrollbar barComponent;

    public GameObject Bar;

    public GameObject BufferedObject{
get{
return bufferedObject;
}
set{
if(value != null){
RotationImage.SetActive(true);
DestroyImage.SetActive(true);
Bar.SetActive(true);
// set properties for Buffered subject and check value of ScrollBar
bufferedObject.GetComponent<SubjectProperties>().Size = barComponent.value;

}else{
    RotationImage.SetActive(false);
    DestroyImage.SetActive(false);
    Bar.SetActive(false);
}
}

    }
public ARPlaneManager ARPlane;
public Text HitInfo;

[Header("Values for set rotation to the BufferedObject")]

[SerializeField]
[Tooltip("Do not edit")]
private float RotX;
[SerializeField]
[Tooltip("Do not edit")]
private float RotY;
[SerializeField]
[Range(0.1f, 1f)]
private float SpeedOfRotate;

public string CurentClassification;
    

    void Start()
    {
        //arOrigin = FindObjectOfType<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        BufferedObject = bufferedObject;
      
        UpdatePlacementPose();
        UpdatePlacementIndicator();
BufferedObjectRotate(RotX,RotY);
        
    }
#region ActionsWithBufferedObject
    protected void BufferedObjectRotate(float ValueOfRotateX,float ValueOfRotateY){
bufferedObject.transform.Rotate(ValueOfRotateX,ValueOfRotateY,0f);
    }

    public void DestroyBufferedObject(){
Destroy(bufferedObject);

    }

    public void ScaleBufferedObject(){
        bufferedObject.transform.localScale = bufferedObject.transform.localScale + new Vector3();
    }
#endregion


    public void CreateAndPlaceObject(){
 if (placementPoseIsValid)
        {
            PlaceObject();
        }

    }

      private void PlaceObject()
    {
       GameObject gm = Instantiate(objectToPlace);
       gm.transform.position = PlacementPose.position;
       gm.transform.rotation = PlacementPose.rotation;
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
		{
            placementIndicator.SetActive(false);
		}
	}

    private void UpdatePlacementPose()
	{
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseIsValid = hits.Count > 0;

     var a = ARPlane.GetPlane(hits[0].trackableId).gameObject.transform.rotation.x;
     MeshDetecting(hits,a);
        if (placementPoseIsValid)
		{
            PlacementPose = hits[0].pose;
		}
	}
private void MeshDetecting(List <ARRaycastHit> hits, float value ){
if(hits[0] != null){

     if(value == 0){
  HitInfo.text = "floor";
  CurentClassification = "Floor";
     }else if(value > 0 || value < 0){
 HitInfo.text = "Wall";
 CurentClassification = "Wall";
     }
     }else{
          HitInfo.text = "UnKnown";
     }
SetSprites();

}

    private void SetSprites(){
int ID = 0;
        foreach(GameObject image in Images){
            
            switch(CurentClassification){
case "Floor":
image.GetComponent<Image>().sprite = selectCategory.category[0].SpriteOfItem[ID];
ID++;
break;
case "Wall":
image.GetComponent<Image>().sprite = selectCategory.category[1].SpriteOfItem[ID];
ID++;
break;
case "UnKnown":
image.GetComponent<Image>().sprite = null;
break;


            }

        }
    }
#region EventsForRotationBufferedObject
public void LeftDown(){
RotX = -SpeedOfRotate;
}
public void RightDown(){
RotX = SpeedOfRotate;
}
public void TopDown(){
RotY = SpeedOfRotate;
}
public void BottomDown(){
RotY = -SpeedOfRotate;
}
public void LeftUp(){
RotX = 0f;
}
public void RightUp(){
RotX = 0f;
}
public void TopUp(){
RotY = 0f;
}
public void BottomUp(){
RotY = 0f;
}



#endregion
    
}