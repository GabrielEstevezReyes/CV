using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSwapper : Selector
{
    public List<Sprite> iconImages = new List<Sprite>();
    protected List<Sprite> tempList;
    public Button leftIcon;
    public Button bigIcon;
    public Button rightIcon;
    Dictionary<string, int> idIcons = new Dictionary<string, int>();

    void Start()
    {
        for (int i = 0; i < iconImages.Count; i++)
        {
            idIcons.Add(iconImages[i].name, i);
        }
        setIcons(iconImages);
        tempList = new List<Sprite>(iconImages);
        
    }

    public override void moveRight()
    {
        onRightClick();
    }

    public override void moveLeft()
    {
        onLeftClick();
    }

     public void setIcons(List<Sprite> iconList)
    {
        leftIcon.GetComponent<Image>().sprite = iconList[0];
        bigIcon.GetComponent<Image>().sprite = iconList[1];
        rightIcon.GetComponent<Image>().sprite = iconList[2];
        bigIcon.name = idIcons[iconList[1].name].ToString();
    }


    public void onLeftClick()
    {
        tempList[0] = iconImages[iconImages.Count - 1];
        for(int i = 1; i < iconImages.Count; i++)
        {
            tempList[i] = iconImages[i - 1];
        }
        setIcons(tempList);
        iconImages = new List<Sprite>(tempList);
    }

    public void onRightClick()
    {
        
        tempList[iconImages.Count - 1] = iconImages[0];
        for (int i = 0; i < iconImages.Count - 1; i++)
        {
            tempList[i] = iconImages[i + 1];            
        }
        setIcons(tempList);
        iconImages = new List<Sprite>(tempList);
    }

    public void printList()
    {
        foreach(Sprite s in iconImages)
        {
            Debug.Log("Icon: " + s.name);
        }

        foreach (Sprite s in tempList)
        {
            Debug.Log("temp: " + s.name);
        }
    }
}
