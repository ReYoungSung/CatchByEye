using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStar : MonoBehaviour
{
    [SerializeField] private Sprite _emptyStarSprite;
    [SerializeField] private Sprite _fullStarSprite;
    [SerializeField] private int _maxScore = 5;

    private List<Image> _stars = new List<Image>();

    private void Start()
    {
        // 별 이미지를 보관할 리스트를 생성합니다.
        for (int i = 0; i < _maxScore; i++)
        {
            GameObject star = new GameObject("Star", typeof(Image));
            star.transform.SetParent(transform);
            star.GetComponent<RectTransform>().localPosition = new Vector3(30 * i, 0, 0);
            Image image = star.GetComponent<Image>();
            image.sprite = _emptyStarSprite;
            _stars.Add(image);
        }
    }

    public void ShowStars(int score)
    {
        // 입력받은 점수에 따라서 별을 채웁니다.
        for (int i = 0; i < _stars.Count; i++)
        {
            if ((i+1) <= score)
            {
                _stars[i].sprite = _fullStarSprite;
            }
            else
            {
                _stars[i].sprite = _emptyStarSprite;
            }
        }
    }
}