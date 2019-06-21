using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;

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

    public TextMesh DebugDisplay;
    public string PrimaryText { get; set; }


    /// <summary>
    /// Result Panel
    /// </summary>
    public List<GameObject> resultPanels;
    public GameObject panelReference;
    public float panelSpawnDistance = 1.0f;

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

    public void Update_DebugDisplay()
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
        Debug.Log("Showing result . . .");
        PrimaryText = "Showing result . . .";
        Update_DebugDisplay();

        if (analysisObject.predictions != null)
        {
            Debug.Log("Prediction is");
            PrimaryText = "Prediction is . . .";
            Update_DebugDisplay();

            // Sort the predictions to locate the highest one
            List<Prediction> sortedPredictions = new List<Prediction>();
            sortedPredictions = analysisObject.predictions.OrderBy(p => p.probability).ToList();
            Prediction bestPrediction = new Prediction();
            bestPrediction = sortedPredictions[sortedPredictions.Count - 1];

            if (bestPrediction.probability > probabilityThreshold)
            {
                //PrimaryText = bestPrediction.tagName + " : " + bestPrediction.probability.ToString();
                PrimaryText = "Take a picture to be analyzed";

                Update_DebugDisplay();

                GameObject resultPanel = Instantiate(panelReference, CameraCache.Main.transform.position, CameraCache.Main.transform.rotation);
                Vector3 toPosition = CameraCache.Main.transform.position + CameraCache.Main.transform.forward;
                resultPanel.transform.position = toPosition;

                PredictionResultData resultData = new PredictionResultData();
                resultData.objectName = bestPrediction.tagName;
                resultData.confidentResult = bestPrediction.probability.ToString();

                StateManagement.Instance.lastestPredictionResult = resultData;

                resultPanels.Add(resultPanel);
            }
        }
        else
        {
            Debug.Log("Object is unknown");
            PrimaryText = "Object is unknown";
            Update_DebugDisplay();
        }

        // Stop the analysis process
        ImageCapture.Instance.ResetImageCapture();
    }
}
