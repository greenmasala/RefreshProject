using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Transition : MonoBehaviour
{
    public RectTransform TransitionImage;
    public RectTransform Text;
    public TextMeshProUGUI LevelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelText.text = $"LEVEL_{SceneManager.GetActiveScene().buildIndex}";
        LevelText.maxVisibleCharacters = 0;
        Sequence s = DOTween.Sequence();
        s.Append(TransitionImage.DOScaleY(0.15f, 0.6f));
        s.InsertCallback(0.4f, () => StartCoroutine(TypeText()));
        s.AppendInterval(0.6f);
        s.Append(TransitionImage.DOScaleY(0f, 0.6f));
        s.Join(Text.DOScaleY(0, 0.2f));
    }

    IEnumerator TypeText()
    {
        int currentCharacterCount = LevelText.textInfo.characterCount + 1;

        for (int i = 0; i < currentCharacterCount; i++)
        {
            LevelText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
