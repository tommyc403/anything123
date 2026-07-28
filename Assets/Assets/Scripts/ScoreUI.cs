using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text ScoreTextBox;
  

    public void RefreshScore(int score)
    { 
        ScoreTextBox.text = score.ToString();    
        
    }
}