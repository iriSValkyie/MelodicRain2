using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolmeContoroller : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource BGMaudioSource;
    [SerializeField] AudioSource SEaudioSource;
    [Header("スライダー")]
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;
    // Start is called before the first frame update
    void Start()
    {
        //初期ボリュームをスライダーに適用
        BGMSlider.value = BGMaudioSource.volume;
        SESlider.value = SEaudioSource.volume;
    }
    // Update is called once per frame
    void Update()
    {
        //スライダーの数値を常時監視
        BGMaudioSource.volume = BGMSlider.normalizedValue;
        SEaudioSource.volume = SESlider.normalizedValue;
    }
    
}
