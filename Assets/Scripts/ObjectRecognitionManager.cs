using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

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
    internal float probabilityThreshold = 0.3f;

    public TextMesh DebugDisplay;
    public string PrimaryText { get; private set; }

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

    void Start()
    {
        // Not Implemented Yet
        PrimaryText = "Take a picture to be analyzed";
        Update_DebugDisplay();
    }

    private void Update_DebugDisplay()
    {
        // Basic checks
        if (DebugDisplay == null)
        {
            return;
        }

        // Update display text
        DebugDisplay.text = PrimaryText;
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
                PrimaryText = bestPrediction.tagName;

                Update_DebugDisplay();

                // Stop the analysis process
                ImageCapture.Instance.ResetImageCapture();
            }
        }
    }
}
