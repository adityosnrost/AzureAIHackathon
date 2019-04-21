using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectRecognitionManager : MonoBehaviour
{
    /// <summary>
    /// Allows this class to behave like a singleton
    /// </summary>
    public static ObjectRecognitionManager Instance;

    /// <summary>
    /// Current threshold accepted for displaying the label
    /// Reduce this value to display the recognition more often
    /// </summary>
    internal float probabilityThreshold = 0.5f;

    private void Awake()
    {
        // Use this class instance as singleton
        Instance = this;

        // Add the ImageCapture class to this Gameobject
        gameObject.AddComponent<ImageCapture>();

        // Add the CustomVisionAnalyser class to this Gameobject
        gameObject.AddComponent<CustomVisionAnalyser>();

        // Add the CustomVisionObjects class to this Gameobject
        gameObject.AddComponent<CustomVisionObjects>();
    }

    public void ObjectAnalysisResult(AnalysisRootObject analysisObject)
    {
        if (analysisObject.predictions != null)
        {
            // Sort the predictions to locate the highest one
            List<Prediction> sortedPredictions = new List<Prediction>();
            sortedPredictions = analysisObject.predictions.OrderBy(p => p.probability).ToList();
            Prediction bestPrediction = new Prediction();
            bestPrediction = sortedPredictions[sortedPredictions.Count - 1];

            if (bestPrediction.probability > probabilityThreshold)
            {
                //bestPrediction.tagName;
            }
        }
    }
}
