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
    Sequence sequence;

    private void OnEnable() => SceneManager.activeSceneChanged += SceneChanged;
    private void OnDisable() => SceneManager.activeSceneChanged -= SceneChanged;

    private void SceneChanged(Scene oldScene, Scene newScene)
    {
        if (oldScene.buildIndex != newScene.buildIndex)
        {
            RunTransition();
        }
    }
    void RunTransition()
    {
        sequence.Restart();
        LevelText.text = $"LEVEL_{SceneManager.GetActiveScene().buildIndex}";
        LevelText.maxVisibleCharacters = 0;
        sequence = DOTween.Sequence();
        sequence.Append(TransitionImage.DOScaleY(0.15f, 0.6f));
        sequence.InsertCallback(0.4f, () => StartCoroutine(TypeText()));
        sequence.AppendInterval(0.6f);
        sequence.Append(TransitionImage.DOScaleY(0f, 0.6f));
        sequence.Join(Text.DOScaleY(0, 0.2f));
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
