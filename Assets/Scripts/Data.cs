using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Data
{
    public int coin;
    public int levelOfMap;
    //skin
    public byte[] albedoTexture;
    public byte[] metallicTexture;
    public byte[] specularTexture;
    public byte[] normalTexture;
    public byte[] heightTexture;
    public float color_r;
    public float color_g;
    public float color_b;
    public float color_a;
    public float metallic;
    public float metallicSmoothness;
    public float bumpScale;
    public float parallax;
    public Data (UserDataManager userManager) //data to save
    {
        this.coin = userManager.coin;
        this.levelOfMap = userManager.levelOfMap;
        this.color_r = userManager.skin.color.r;
        this.color_g = userManager.skin.color.g;
        this.color_b = userManager.skin.color.b;
        this.color_a = userManager.skin.color.a;
        this.metallic = userManager.skin.GetFloat("_Metallic");
        this.bumpScale = userManager.skin.GetFloat("_BumpScale");
        this.parallax = userManager.skin.GetFloat("_Parallax");
        this.metallicSmoothness = userManager.skin.GetFloat("_Smoothness"); //both use metallic vs specular
        if (userManager.skin.mainTexture != null)
        {
            ConvertTexture(userManager.skin.mainTexture);
        }
        if (userManager.skin.GetTexture("_MetallicGlossMap") != null)
        {
            ConvertTexture(userManager.skin.GetTexture("_MetallicGlossMap"));
        }
        if (userManager.skin.GetTexture("_SpecGlossMap") != null)
        {
            ConvertTexture(userManager.skin.GetTexture("_SpecGlossMap"));
        }
        if (userManager.skin.GetTexture("_BumpMap") != null)
        {
            ConvertTexture(userManager.skin.GetTexture("_BumpMap"));
        }
        if (userManager.skin.GetTexture("_ParallaxMap") != null)
        {
            ConvertTexture(userManager.skin.GetTexture("_ParallaxMap"));
        }
    }
    private void ConvertTexture(Texture tex)
    {
        Texture2D uncompressedTex = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);
        uncompressedTex.SetPixels((tex as Texture2D).GetPixels());
        uncompressedTex.Apply();
    /*    this.albedoTexture = ImageConversion.EncodeToPNG(uncompressedTex);
        this.metallicTexture = ImageConversion.EncodeToPNG(uncompressedTex);
        this.specularTexture = ImageConversion.EncodeToPNG(uncompressedTex);
        this.normalTexture = ImageConversion.EncodeToPNG(uncompressedTex);
        this.heightTexture = ImageConversion.EncodeToPNG(uncompressedTex);*/
    }
}
