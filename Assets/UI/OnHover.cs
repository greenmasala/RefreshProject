using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI buttonName;
    Color ogButtonColor;
    Color hoverColor;
    [SerializeField] List<Image> changeColor = new List<Image>();
    private void Start()
    {
        buttonName = GetComponentInChildren<TextMeshProUGUI>();
        ogButtonColor = buttonName.color;
        hoverColor = new Color(0.03529412f, 0.07450981f, 0.003921569f);

        buttonName.maxVisibleCharacters = 0;
        StartCoroutine(TypeText());

        var changeColor = GetComponentsInChildren<Image>().ToList();
        changeColor.RemoveAt(0);
        this.changeColor = changeColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(TypeText());
        Debug.Log("hOVERING");
        Color c = changeColor[0].color;
        c.a = 1;
        changeColor[0].color = c;

        buttonName.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color c = changeColor[0].color;
        c.a = 0;
        changeColor[0].color = c;

        buttonName.color = ogButtonColor;
    }
    IEnumerator TypeText()
    {
        yield return new WaitForSeconds(0.05f);
        int currentCharacterCount = buttonName.textInfo.characterCount + 1;

        for (int i = 0; i < currentCharacterCount; i++)
        {
            buttonName.maxVisibleCharacters = i;
            Debug.Log(i);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
