using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagement : Singleton<StateManagement>
{
    public PredictionResultData lastestPredictionResult = null;
}
