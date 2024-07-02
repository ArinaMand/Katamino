using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTriggering : MonoBehaviour
{
    public delegate void DragEndedDelagate(QuadTriggering quad);
    public DragEndedDelagate dragCallback;
    
    public bool is_filled = false;
    public int collis = 0;

    private void OnTriggerEnter(Collider other){
        if (!is_filled){
            is_filled = true;
            dragCallback(this);
        }
        else{
            collis+=1;
        }
    }
    private void OnTriggerExit(Collider other){
        if (collis>0){
            collis-=1;
        }
        else{
            is_filled = false;
            dragCallback(this);
        }
    }
}
