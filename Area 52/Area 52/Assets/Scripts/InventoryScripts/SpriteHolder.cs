using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I need this class, because the sprite isn't serializable so it can't be saved in the BinaryFormatter
//I don't use a dictionary, because it's not serializable and I can't change it in the Unity Editor
//You must create a GameObject in the scene and add all names and sprites in the lists, but name's index must match the sprite's one
public class SpriteHolder : MonoBehaviour
{
    public List<string> names;
    public List<Sprite> sprites;

    public Sprite getSprite(string name)
    {
        foreach(string s in names)
        {
            if (s.Contains(name))
            {
                return sprites[names.IndexOf(s)];
            }
        }
        return null;
    }

}

