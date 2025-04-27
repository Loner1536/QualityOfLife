using UnityEngine;
using System.Collections.Generic;
using System.IO;
using MelonLoader;
using MelonLoader.Utils;

namespace QualityOfLife.Interface.IconLoaders.Components;

public static class DrugIconLoader
{
    public static Dictionary<string, Sprite> Load()
    {
        Dictionary<string, Sprite> drugIcons = new Dictionary<string, Sprite>();
        string iconDirectory = Path.Combine(MelonEnvironment.UserDataDirectory, "QualityOfLife", "Icons", "Drugs");

        if (!Directory.Exists(iconDirectory))
        {
            MelonLogger.Error($"[DrugIconLoader] Directory not found: {iconDirectory}");
            return drugIcons;
        }

        string[] pngFiles = Directory.GetFiles(iconDirectory, "*.png");

        foreach (string filePath in pngFiles)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            Texture2D texture = LoadTexture(filePath);
            if (texture != null)
            {
                drugIcons[fileNameWithoutExtension] = TextureToSprite(texture);
            }
        }
        return drugIcons;
    }

    private static Texture2D LoadTexture(string filePath)
    {
        Texture2D texture = null;
        byte[] fileData;
        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
        }
        return texture;
    }

    private static Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
