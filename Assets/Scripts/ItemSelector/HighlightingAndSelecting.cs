using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightingAndSelecting : MonoBehaviour, IPointerClickHandler
{
    public int ImageID;
    public List<Image> AllImages = new List<Image>();
    public ARTapToPlaceObject ARtaptoplaceobject;
    public SelectCategory selectCategory;
    public void OnPointerClick(PointerEventData eventData)
    {
      ClerHightLighting(AllImages);
      gameObject.GetComponent<Image>().color = Color.red;
      SetSelectedItem(ARtaptoplaceobject.CurentClassification);
    }

    private void ClerHightLighting(List<Image> images){
foreach(Image image in images){
image.color = Color.white;

}

    }

    private void SetSelectedItem(string CurentClassification){
    switch(CurentClassification){
case "Floor":
ARtaptoplaceobject.objectToPlace = selectCategory.category[0].PrefabOfItem[ImageID];

break;
case "Wall":
ARtaptoplaceobject.objectToPlace = selectCategory.category[1].PrefabOfItem[ImageID];

break;
case "UnKnown":
ARtaptoplaceobject.objectToPlace = null;
break;


            }
    }
}
