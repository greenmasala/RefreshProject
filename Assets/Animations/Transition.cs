using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor.Animations;

public class Transition : MonoBehaviour
{
    public RectTransform TransitionImage;
    public RectTransform Text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sequence s = DOTween.Sequence();
        s.Append(TransitionImage.DOScaleY(0.15f, 0.8f).SetEase(Ease.InOutElastic, 0.1f));
        s.Insert(0.65f, Text.DOScaleY(1, 0.2f)).AppendInterval(0.6f); 
        s.Append(TransitionImage.DOScaleY(0f, 0.6f));
        s.Join(Text.DOScaleY(0, 0.2f));
    }
}
