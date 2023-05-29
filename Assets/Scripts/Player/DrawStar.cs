using UnityEngine;
using System.Collections;
 
public class DrawStar : MonoBehaviour {
    public Texture2D texture;
    // Use this for initialization
    void OnGUI()
    {
        Rect rect = new Rect(
        
        Screen.width/2 - (texture.width/6),

        Screen.height/2 - (texture.height/6),
 
        texture.width/3, texture.height/3);
 
        GUI.DrawTexture(rect, texture);
    }
}