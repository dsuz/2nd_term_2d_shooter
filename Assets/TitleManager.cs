using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Initiate.Fade(sceneName, Color.black, 2.5f);
    }
}
