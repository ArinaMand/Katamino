using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private Vector3 cuteStartRotation;
    bool flag = false;
    public bool is_placed = false;
    


    public delegate void DragEndedDelagate(DragObject dragobj);
    public DragEndedDelagate dragCallback;
    
    void OnMouseDown(){
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        cuteStartRotation = gameObject.transform.eulerAngles;
        flag = true;

    }
    private Vector3 GetMouseWorldPos(){
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag(){
        Vector3 pos = GetMouseWorldPos();
        transform.position = new Vector3(pos.x+mOffset.x, gameObject.transform.position.y, pos.z+mOffset.z);
        /*if (is_placed){
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }*/
    }

    void OnMouseUp(){
        flag = false;
        transform.position = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
        dragCallback(this);
    }

    void OnGUI()
    {
        if (flag){
            if (Event.current.Equals(Event.KeyboardEvent("D"))) {
                transform.Rotate(0, 0, 90f);
            }
            if (Event.current.Equals(Event.KeyboardEvent("A"))) {
                transform.Rotate(0, 0, 90f);
            }
            if (Event.current.Equals(Event.KeyboardEvent("W"))) {
                transform.Rotate(90f, 0, 0);
            }
            if (Event.current.Equals(Event.KeyboardEvent("S"))) {
                transform.Rotate(90f, 0, 0);
            }
        }
    }

    void OnCollisionEnter(Collision obj){
        if (is_placed){

        }
    }
}
