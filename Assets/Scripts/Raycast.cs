using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
   public ARTapToPlaceObject ARTTPO;
bool SaveScrollBarSize = true;
   
    void Update()
    {
         RaycastHit hit;
         
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
           
           if(hit.collider.gameObject.tag == "Subject"){
ARTTPO.bufferedObject = hit.collider.gameObject;
ARTTPO.HitInfo.text += hit.collider.gameObject.name;
if(SaveScrollBarSize){
    SaveScrollBarSize = false;
ARTTPO.barComponent.value = hit.collider.gameObject.GetComponent<SubjectProperties>().Size;

}
           }else{
               ARTTPO.bufferedObject = null;
               ARTTPO.barComponent.value = 0f;
               SaveScrollBarSize = true;
           }
        }
        else
        {
           
        }
    }
}
