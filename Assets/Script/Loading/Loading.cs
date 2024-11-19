using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public static int nScene;
    public Slider mySlider;
    void Start()
    {
        StartCoroutine(LoadingScene());
    }

    public static void LoadScene(int n)
    {
        nScene = n;
        SceneManager.LoadScene(1);
    }

    IEnumerator LoadingScene()
    {
        mySlider.value = 0.0f;
        yield return GameTime.Wait(0.3f);
        // AsyncOperation: 얼마만큼 진행되었는지 알 수 있음
        // progress : 진행정도 float 최대 0.9f
        // allowSceneActivation : 로드완료 시 바로 넘어갈지 bool
        AsyncOperation ao = SceneManager.LoadSceneAsync(nScene);
        ao.allowSceneActivation = false;
        while (mySlider.value < mySlider.maxValue)
        {
            yield return StartCoroutine(UpdateSlider(ao.progress));
        }
        yield return GameTime.Wait(0.3f);
        ao.allowSceneActivation = true;
    }

    IEnumerator UpdateSlider(float v)
    {
        while (mySlider.value < v)
        {
            mySlider.value += Time.deltaTime;
            yield return null;
        }
        mySlider.value = v;
    }
}
