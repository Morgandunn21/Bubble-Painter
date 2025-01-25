using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public LevelData levelData;
    public Painting painting;
    public float colorThreshold = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputHandler.Instance.OnSubmitInput.AddListener(OnSubmit);
    }

    public void OnSubmit(InputValue value)
    {
        if (value.isPressed) EndLevel();
    }

    public void EndLevel()
    {
        float score = CalculateScore(levelData.GoalTexture, painting.GetPainting());
        int stars = CalculateStars(levelData.ScoreThresholds, score);

        Debug.Log($"Percent: {(int)(score * 100)}");
        Debug.Log($"Stars: {stars}");
    }

    private float CalculateScore(Texture2D GoalTexture, Texture2D PlayerTexture)
    {
        int pixelCount = 0;
        int correctCount = 0;

        for (int i = 0; i < GoalTexture.width; i++)
        {
            for (int j = 0; j < GoalTexture.height; j++)
            {
                pixelCount++;
                if (SameColor(GoalTexture.GetPixel(i, j), PlayerTexture.GetPixel(j, i))) correctCount++;
            }
        }

        return (float)correctCount / pixelCount;
    }

    private int CalculateStars(float[] thresholds, float score)
    {
        int result = -1;

        for(int i = 0; i < thresholds.Length; i++)
        {
            if (thresholds[i] < score) result = i;
        }

        return result+1;
    }

    public bool SameColor(Color a, Color b)
    {
        bool result = true;

        result = result && Mathf.Abs(a.r - b.r) <= colorThreshold;
        result = result && Mathf.Abs(a.g - b.g) <= colorThreshold;
        result = result && Mathf.Abs(a.b - b.b) <= colorThreshold;

        return result;
    }
}
