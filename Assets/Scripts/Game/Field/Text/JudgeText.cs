using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JudgeText : MonoBehaviour
{
    [SerializeField] private TextMeshPro[] judgeTexts;
    
    public void Show(JudgeEnum judge)
    {
        
        var color = judgeTexts[(int)judge].color;
        judgeTexts[(int)judge].color = new Color(color.r, color.g, color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var judgeText in judgeTexts)
        {
            var color = judgeText.color;
            var a = color.a - Time.deltaTime * 5;
            judgeText.color = new Color(color.r, color.g, color.b, Mathf.Max(a, 0));
        }

    }
}
