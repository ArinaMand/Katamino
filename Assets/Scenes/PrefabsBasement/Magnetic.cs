using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magnetic : MonoBehaviour
{
    public List<Transform> snapQuads;
    public List<DragObject> draggableBlocks;
    public List<QuadTriggering> TriggeringQuads;
    public GameObject text;

    public float snapRange = 3f;
    private int triggeredQuads;
    public float timeRemaining = 3;

    public Material collectMaterial;

    void Start(){
        Disable();
        foreach(DragObject block in draggableBlocks){
            block.dragCallback = onDragEnd;
        }
        foreach(QuadTriggering quad in TriggeringQuads){
            quad.dragCallback = onBlockPlacing;
        }
        triggeredQuads = 0;
    }

    private void onDragEnd(DragObject block){
        float minDist = 1;
        Transform closestSnapQuad = null;
        foreach (Transform quad in snapQuads){
            float dist = Vector3.Distance(block.transform.position, quad.position);
            if (dist <minDist || closestSnapQuad == null){
                closestSnapQuad = quad;
                minDist = dist;
            }
        }
        if (closestSnapQuad != null && minDist<=snapRange){
            block.transform.position = new Vector3(
                closestSnapQuad.position.x,
                closestSnapQuad.position.y+1.5f,
                closestSnapQuad.position.z
            );
            block.is_placed = true;
        }
    }
    private void onBlockPlacing(QuadTriggering quad){
        if (quad.is_filled){
            triggeredQuads+=1;
        }
        else{
            triggeredQuads-=1;
            Debug.Log("hahaha");
        }
        Debug.Log(triggeredQuads);
    }

    public void Enable(){
        text.SetActive(true);
    }
    public void Disable(){
        text.SetActive(false);
    }

    void Update(){
        if (triggeredQuads == 30){
            Enable();
            if (timeRemaining >0){
                timeRemaining -=Time.deltaTime;
            }
            else{
                timeRemaining = 5;
                triggeredQuads = 0;
                Disable();
                string sceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            }
        }
    }
}
