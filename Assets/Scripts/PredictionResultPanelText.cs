using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredictionResultPanelText : MonoBehaviour
{
    private string shownDesc;

    // Start is called before the first frame update
    void Start()
    {
        Text objectText = gameObject.GetComponent<Text>();

        string xboxDesc = "Equip yourself with the Xbox Wireless Controller, featuring a sleek, streamlined design and textured grip for enhanced comfort. Enjoy custom button mapping and up to twice the wireless range. Plug in any compatible headset with the 3.5mm stereo headset jack. And with Bluetooth® technology, play your favorite games on Windows 10 PCs and tablets.*";
        string balloonDesc = "Latex balloons may be inflated with either air or helium. Because latex is a porous material, the gas (helium or air) molecules pass through the surface, eventually causing the balloon to deflate or descend. When air-inflated, latex balloons stay inflated considerably longer than those inflated with helium because air molecules are larger and slower moving than helium molecules, so air doesn't escape as quickly as helium.";

        if (StateManagement.Instance.lastestPredictionResult != null)
        {
            PredictionResultData resultData = StateManagement.Instance.lastestPredictionResult;

            if (resultData.objectName == "Balloon")
            {
                shownDesc = balloonDesc;
            }
            else if (resultData.objectName == "XboxController")
            {
                shownDesc = xboxDesc;
            }
            else
            {
                shownDesc = "None Desc";
            }

                switch (name)
            {
                case "ResultObjectName":
                    objectText.text = resultData.objectName;
                    break;
                case "ResultObjectDesc":
                    //objectText.text = resultData.description;
                    objectText.text = shownDesc;
                    break;
                case "ConfidentValue":
                    objectText.text = resultData.confidentResult;
                    break;
                default:
                    objectText.text = "Unidentified";
                    break;
            }
        }
    }
}
