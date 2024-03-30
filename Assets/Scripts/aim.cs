using UnityEngine;
using System.Collections;
 
public class DrawStar : MonoBehaviour {
    public Texture2D texture;
    // Use this for initialization
    void OnGUI()
    {
        Rect rect = new Rect((Screen.width/2)-28, (Screen.height/2)-32, texture.width, texture.height);
 
        GUI.DrawTexture(rect, texture);
    }
}