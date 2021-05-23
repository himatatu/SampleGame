using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AsObservable()
            .Subscribe(_ => SceneManager.LoadScene("BattleScene"));
        black.GetComponent<Image>().DOFade(0, 1);
        DOVirtual.DelayedCall(1, () => black.SetActive(false));
    }
}
