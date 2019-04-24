using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonsPages : MonoBehaviour
{
    public enum EShadow
    {
        TWO_BUTTONS_LEFT_SELECTED,
        TWO_BUTTONS_RIGHT_SELECTED,
        THREE_BUTTONS_LEFT_SELECTED,
        THREE_BUTTONS_CENTER_SELECTED,
        THREE_BUTTONS_RIGHT_SELECTED
    };

    public bool manageButtons;
    public bool manageTexts;
    public bool manageShadows;
    public List<TextMeshProUGUI> texts;
    public List<Button> buttons;
    public List<Sprite> activeButtons;
    public List<Sprite> passiveButtons;
    public List<RectTransform> shadows;
    public Color activo;
    public Color pasivo;
    public Color activoText;
    public Color pasivoText;
    public bool accepImage;

    protected float[] initialAnchor;

    private void Awake()
    {
        initialAnchor = new float[shadows.Count];

        for (int i = 0; i < shadows.Count; i++)
        {
            initialAnchor[i] = shadows[i].anchorMin.x;
        }
        initButtons();
        initTexts();
    }

    protected void initButtons()
    {
        if (manageButtons)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<Image>().sprite = passiveButtons[i];
            }
        }
    }

    protected void initTexts()
    {
        if (manageTexts)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].color = pasivoText;
            }
        }
    }

    public void updateButtons(Button boton)
    {
        if (manageButtons)
        {
            Debug.Log("UpdateButton");
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].gameObject != boton.gameObject)
                {
                    buttons[i].GetComponent<Image>().sprite = passiveButtons[i];
                }
                else
                {
                    Debug.Log(i);
                    buttons[i].GetComponent<Image>().sprite = activeButtons[i];
                }
            }
        }
    }

    public void updateTexts(TextMeshProUGUI text)
    {
        if (manageTexts)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                if (texts[i].text != text.text)
                {
                    texts[i].color = pasivoText;
                }
                else
                {
                    texts[i].color = activoText;
                }
            }
        }

    }

    public void updateshadows(int estado)
    {
        EShadow myEnum = (EShadow)estado;
        if (manageShadows)
        {
            switch (myEnum)
            {
                case EShadow.TWO_BUTTONS_LEFT_SELECTED:
                    float newAnchorMinX = initialAnchor[0];
                    float newAnchorMaxX = initialAnchor[0] + 0.01f;
                    shadows[0].anchorMax = new Vector2(newAnchorMaxX, shadows[0].anchorMax.y);
                    shadows[0].anchorMin = new Vector2(newAnchorMinX, shadows[0].anchorMin.y);
                    shadows[0].localScale = Vector3.one;
                    shadows[0].ResetOffsets();
                    break;
                case EShadow.TWO_BUTTONS_RIGHT_SELECTED:
                    float newAnchorMinX1 = initialAnchor[0] - 0.01f;
                    float newAnchorMaxX1 = initialAnchor[0];
                    shadows[0].anchorMax = new Vector2(newAnchorMaxX1, shadows[0].anchorMax.y);
                    shadows[0].anchorMin = new Vector2(newAnchorMinX1, shadows[0].anchorMin.y);
                    shadows[0].localScale = new Vector3(-1,1,1);
                    shadows[0].ResetOffsets();
                    break;
                case EShadow.THREE_BUTTONS_LEFT_SELECTED:
                    float newAnchorMinX2 = initialAnchor[0];
                    float newAnchorMaxX2 = initialAnchor[0] + 0.01f;
                    shadows[0].anchorMax = new Vector2(newAnchorMaxX2, shadows[0].anchorMax.y);
                    shadows[0].anchorMin = new Vector2(newAnchorMinX2, shadows[0].anchorMin.y);
                    shadows[0].localScale = Vector3.one;
                    shadows[0].ResetOffsets();
                    break;
                case EShadow.THREE_BUTTONS_CENTER_SELECTED:
                    float newAnchorMinX3 = initialAnchor[0] - 0.01f;
                    float newAnchorMaxX3 = initialAnchor[0];
                    shadows[0].anchorMax = new Vector2(newAnchorMaxX3, shadows[0].anchorMax.y);
                    shadows[0].anchorMin = new Vector2(newAnchorMinX3, shadows[0].anchorMin.y);
                    shadows[0].localScale = new Vector3(-1, 1, 1);
                    shadows[0].ResetOffsets();

                    float newAnchorMinX4 = initialAnchor[1];
                    float newAnchorMaxX4 = initialAnchor[1] + 0.01f;
                    shadows[1].anchorMax = new Vector2(newAnchorMaxX4, shadows[1].anchorMax.y);
                    shadows[1].anchorMin = new Vector2(newAnchorMinX4, shadows[1].anchorMin.y);
                    shadows[1].localScale = new Vector3(-1, 1, 1);
                    shadows[1].ResetOffsets();
                    break;
                case EShadow.THREE_BUTTONS_RIGHT_SELECTED:
                    float newAnchorMinX6 = initialAnchor[1] - 0.01f;
                    float newAnchorMaxX6 = initialAnchor[1];
                    shadows[1].anchorMax = new Vector2(newAnchorMaxX6, shadows[1].anchorMax.y);
                    shadows[1].anchorMin = new Vector2(newAnchorMinX6, shadows[1].anchorMin.y);
                    shadows[1].localScale = Vector3.one;
                    shadows[1].ResetOffsets();
                    break;
            }
        }
    }
}
