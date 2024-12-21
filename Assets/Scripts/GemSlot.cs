using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemSlot : MonoBehaviour
{
void Update() {
    DragandDropImage.gemSlot1 = DragandDropImage.GetGemSlot1();
    DragandDropImage.gemSlot2 = DragandDropImage.GetGemSlot2();
    if(DragandDropImage.slotCount == 2){
        if( DragandDropImage.gemSlot1 == "Blue" && DragandDropImage.gemSlot2 == "Blue" ){
            Debug.Log("SuperBlue");
        }
        if( DragandDropImage.gemSlot1 == "Red" && DragandDropImage.gemSlot2 == "Red" ){
            Debug.Log("SuperRed");
        }
         if( DragandDropImage.gemSlot1 == "Yellow" && DragandDropImage.gemSlot2 == "Yellow" ){
            Debug.Log("SuperYellow");
        }
        DragandDropImage.slotCount=0;
    }
}

}