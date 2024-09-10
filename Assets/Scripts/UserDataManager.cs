using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
public class UserDataManager : Singleton<UserDataManager>
{
    public bool isLoading;
    public int coin;
    public int levelOfMap;
    public Material skin;
    public void Save()
   {
        isLoading = false;
        SaveSystem.Save(this);
   }
    public void Load() //data to load
    {
        isLoading = true;
        Data data = SaveSystem.Load(); //get data from saved file
        coin = data.coin;
        levelOfMap = data.levelOfMap; 
        skin.color = new Color(data.color_r, data.color_g, data.color_b, data.color_a);
        Texture2D tex = new Texture2D(1960, 1080);
        //Base Map
        if (data.albedoTexture != null)
        {
            tex.LoadImage(data.albedoTexture);
            skin.mainTexture = tex;
        }
        //Metallic Map
        if (data.metallicTexture != null)
        {
            tex.LoadImage(data.metallicTexture);
            skin.SetTexture("_MetallicGlossMap", tex);
        }
        //Specular Map
        if (data.specularTexture != null)
        {
            tex.LoadImage(data.specularTexture);
            skin.SetTexture("_SpecGlossMap", tex);
        }
        //Normal Map
        if (data.normalTexture != null)
        {
            tex.LoadImage(data.specularTexture);
            skin.SetTexture("_BumpMap", tex);
        }
        //Height Map
        if (data.heightTexture != null)
        {
            tex.LoadImage(data.specularTexture);
            skin.SetTexture("_ParallaxMap", tex);
        }
        //Metallic Scale
        skin.SetFloat("_Metallic", data.metallic);
        //Smoothness Scale
        skin.SetFloat("_Smoothness", data.metallicSmoothness);
        //Normal Scale
        skin.SetFloat("_BumpScale", data.bumpScale);
        //Height Scale
        skin.SetFloat("_Parallax", data.parallax);
        Invoke("isLoadingOff", .1f);
    }
    private void isLoadingOff()
    {
        isLoading = false;
    }
}
