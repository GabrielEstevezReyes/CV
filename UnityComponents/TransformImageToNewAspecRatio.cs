using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformImageToNewAspecRatio
{
    private static int sizingOnX(Vector2 desiredAspect, float width, float height/*0 for no resizing, 1 for resizing on x, 2 for y*/)
    {
        float actualRatio = width / height;
        float desired = desiredAspect.x / desiredAspect.y;
        if (desired == actualRatio)
        {
            return 0;
        }
        else if (desired > actualRatio)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    protected static Vector2 getNewSize(Vector2 desiredAspect, float width, float height)
    {
        int resizeIndicator = sizingOnX(desiredAspect, width, height);
        if(resizeIndicator == 0)
        {
            return new Vector2(width, height);
        }
        else if(resizeIndicator == 1){
            return new Vector2(((desiredAspect.x * height) / desiredAspect.y), height);
        }
        else
        {
            return new Vector2(width, ((desiredAspect.y * width) / desiredAspect.x));
        }
    }

    private static Texture2D fillImageOnX(int desiredX, Texture2D originalImageParameter)
    {
        Texture2D newImage = new Texture2D(desiredX, originalImageParameter.height);
        //Debug.Log(desiredX + " " + originalImageParameter.height);
        int diferenceInSize = (desiredX - originalImageParameter.width) / 2;
        Color[] newPixels = new Color[desiredX * originalImageParameter.height];

        int tempIndex = 0;

        for(int i = 0; i < desiredX; i++)
        {
            for(int j = 0; j < (int)originalImageParameter.height; j++)
            {
                tempIndex = i + (desiredX * j);
                if (i < diferenceInSize || i >= diferenceInSize + originalImageParameter.width)
                {
                    newPixels[tempIndex] = Color.white;
                }
                else
                {
                    newPixels[tempIndex] = originalImageParameter.GetPixel(i - diferenceInSize, j);
                }
            }
        }

        newImage.SetPixels(newPixels);
        newImage.Apply();
        return newImage;
    }

    private static Texture2D fillImageOnY(int desiredY, Texture2D originalImageParameter)
    {
        Texture2D newImage = new Texture2D(originalImageParameter.width, desiredY);
        int diferenceInSize = (desiredY - originalImageParameter.height) / 2;
        Color[] newPixels = new Color[desiredY * originalImageParameter.width];
        int tempIndex = 0;

        for (int i = 0; i < diferenceInSize * originalImageParameter.width; i++)
        {
            newPixels[i] = Color.white;
        }

        for (int i = 0; i < originalImageParameter.width; i++)
        {
            for (int j = 0; j < originalImageParameter.height; j++)
            {
                tempIndex = i + (originalImageParameter.width * j) + diferenceInSize * originalImageParameter.width;
                newPixels[tempIndex] = originalImageParameter.GetPixel(i, j);
            }
        }

        for (int i = (originalImageParameter.width * originalImageParameter.height) + (diferenceInSize * originalImageParameter.width); i < originalImageParameter.width * desiredY; i++)
        {
            newPixels[i] = Color.white;
        }

        newImage.SetPixels(newPixels);
        newImage.Apply();
        return newImage;
    }

    private static Texture2D fillWithBlankTexture(Vector2 desiredSize, Texture2D originalImage)
    {
        if (desiredSize == new Vector2(originalImage.width, originalImage.height))
        {
            return originalImage;
        }
        else if (desiredSize.x > originalImage.width)
        {
            return fillImageOnX((int)desiredSize.x, originalImage);
        }
        else
        {
            return fillImageOnY((int)desiredSize.y, originalImage);
        }
    }

    private static Texture2D fillWithBlankTexture(Vector2 desiredSize, Texture2D originalImage, Color colorToFill)
    {
        if (desiredSize == new Vector2(originalImage.width, originalImage.height))
        {
            return originalImage;
        }
        else if (desiredSize.x > originalImage.width)
        {
            return fillImageOnX((int)desiredSize.x, originalImage, colorToFill);
        }
        else
        {
            return fillImageOnY((int)desiredSize.y, originalImage, colorToFill);
        }
    }

    private static Texture2D fillImageOnX(int desiredX, Texture2D originalImageParameter, Color fill)
    {
        Texture2D newImage = new Texture2D(desiredX, originalImageParameter.height);
        //Debug.Log(desiredX + " " + originalImageParameter.height);
        int diferenceInSize = (desiredX - originalImageParameter.width) / 2;
        Color[] newPixels = new Color[desiredX * originalImageParameter.height];

        int tempIndex = 0;

        for (int i = 0; i < desiredX; i++)
        {
            for (int j = 0; j < (int)originalImageParameter.height; j++)
            {
                tempIndex = i + (desiredX * j);
                if (i < diferenceInSize || i >= diferenceInSize + originalImageParameter.width)
                {
                    newPixels[tempIndex] = fill;
                }
                else
                {
                    newPixels[tempIndex] = originalImageParameter.GetPixel(i - diferenceInSize, j);
                }
            }
        }

        newImage.SetPixels(newPixels);
        newImage.Apply();
        return newImage;
    }

    private static Texture2D fillImageOnY(int desiredY, Texture2D originalImageParameter, Color fill)
    {
        Texture2D newImage = new Texture2D(originalImageParameter.width, desiredY);
        int diferenceInSize = (desiredY - originalImageParameter.height) / 2;
        Color[] newPixels = new Color[desiredY * originalImageParameter.width];
        int tempIndex = 0;

        for (int i = 0; i < diferenceInSize * originalImageParameter.width; i++)
        {
            newPixels[i] = fill;
        }

        for (int i = 0; i < originalImageParameter.width; i++)
        {
            for (int j = 0; j < originalImageParameter.height; j++)
            {
                tempIndex = i + (originalImageParameter.width * j) + diferenceInSize * originalImageParameter.width;
                newPixels[tempIndex] = originalImageParameter.GetPixel(i, j);
            }
        }

        for (int i = (originalImageParameter.width * originalImageParameter.height) + (diferenceInSize * originalImageParameter.width); i < originalImageParameter.width * desiredY; i++)
        {
            newPixels[i] = Color.white;
        }

        newImage.SetPixels(newPixels);
        newImage.Apply();
        return newImage;
    }

    public static Texture2D transformImage(Vector2 desiredAspectRatio, Texture2D imageToTransform)
    {
        Vector2 newSize = getNewSize(desiredAspectRatio, imageToTransform.width, imageToTransform.height);
        return fillWithBlankTexture(newSize, imageToTransform);
    }

    public static Texture2D transformImage(Vector2 desiredAspectRatio, Texture2D imageToTransform, Color colorToRefill)
    {
        Vector2 newSize = getNewSize(desiredAspectRatio, imageToTransform.width, imageToTransform.height);
        return fillWithBlankTexture(newSize, imageToTransform, colorToRefill);
    }
}
