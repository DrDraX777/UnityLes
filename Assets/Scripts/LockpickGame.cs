using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Linq;

public class LockpickGame : MonoBehaviour
{
    public float countdownTime = 30f;
    private float currentTime;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI Pin1Text;
    public TextMeshProUGUI Pin2Text;
    public TextMeshProUGUI Pin3Text;
    public GameObject ResultPanel;
    public TextMeshProUGUI ResultText;
    public float[] lockPinNumbers = new float[3];
    private float[] TempLockPinNumbers;
    public float[] DrillNumbers = new float[3];
    public float[] HammerNumbers = new float[3];
    public float[] lockPickNumbers = new float[3];
    public TextMeshProUGUI DrillNumbersText;
    public TextMeshProUGUI HammerNumbersText;
    public TextMeshProUGUI lockPickNumberText;
    public int opennumber = 5;
    bool allValid = true;
    bool win = false;
    private void Start()
    {
        currentTime = countdownTime;
        TempLockPinNumbers = (float[])lockPinNumbers.Clone();
        ButtonTextUpdate();
        pinupdate();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(currentTime).ToString();
        }
        else if ((currentTime <= 0) && (!win))
        {
            currentTime = 0;
            countdownText.text = "0";
            ResultPanel.SetActive(true);
            ResultText.text = "Вы Проиграли";
        }

    }

    public void restart()
    {
        allValid = true;
        win = false;
        currentTime = countdownTime;
        countdownText.text = Mathf.Ceil(currentTime).ToString();
        lockPinNumbers = (float[])TempLockPinNumbers.Clone();
        if (ResultPanel.activeSelf)
        {
            ResultPanel.SetActive(false);
        }
        pinupdate();

    }

    public void OnClickDrill()
    {
        for (int i = 0; i < lockPinNumbers.Length; i++)
        {
            if ((lockPinNumbers[i] + DrillNumbers[i]) < 0 || (lockPinNumbers[i] + DrillNumbers[i]) > 10)
            {
                allValid = false;
                break;
            }
            else
            {
                allValid = true;
            }
        }

        if (allValid)
        {
            for (int i = 0; i < lockPinNumbers.Length; i++)
            {
                lockPinNumbers[i] += DrillNumbers[i];
            }
        }

        pinupdate();
        check();
    }

    public void OnClickHammer()
    {
        for (int i = 0; i < lockPinNumbers.Length; i++)
        {
            if ((lockPinNumbers[i] + HammerNumbers[i]) < 0 || (lockPinNumbers[i] + HammerNumbers[i]) > 10)
            {
                allValid = false;
                break;
            }
            else
            {
                allValid = true;
            }
        }

        if (allValid)
        {
            for (int i = 0; i < lockPinNumbers.Length; i++)
            {
                lockPinNumbers[i] += HammerNumbers[i];
            }
        }

        pinupdate();
        check();
    }

    public void OnClickLockPick()
    {
        for (int i = 0; i < lockPinNumbers.Length; i++)
        {
            if ((lockPinNumbers[i] + lockPickNumbers[i]) < 0 || (lockPinNumbers[i] + lockPickNumbers[i]) > 10)
            {
                allValid = false;
                break;
            }
            else
            {
                allValid = true;
            }
        }

        if (allValid)
        {
            for (int i = 0; i < lockPinNumbers.Length; i++)
            {
                lockPinNumbers[i] += lockPickNumbers[i];
            }
        }
        pinupdate();
        check();
    }

    private void pinupdate()
    {
        Pin1Text.text = lockPinNumbers[0].ToString();
        Pin2Text.text = lockPinNumbers[1].ToString();
        Pin3Text.text = lockPinNumbers[2].ToString();
    }

    private void ButtonTextUpdate()
    {
        string formattedNumbers1 = string.Join("|", DrillNumbers.Select(x => x.ToString("+#;-#;0")));
        DrillNumbersText.text = $"{formattedNumbers1}\nДрель";

        string formattedNumbers2 = string.Join("|", HammerNumbers.Select(x => x.ToString("+#;-#;0")));
        HammerNumbersText.text = $"{formattedNumbers2}\nМолоток";

        string formattedNumbers3 = string.Join("|", lockPickNumbers.Select(x => x.ToString("+#;-#;0")));
        lockPickNumberText.text = $"{formattedNumbers3}\nОтмычка";

    }

    private void check()
    {
        if (lockPinNumbers.All(x => x == 5))
        {
            win = true;
            victory();
        }
        else
        {
            win = false;
        }
    }
    private void victory()
    {
        currentTime = 0;
        countdownText.text = "0";
        ResultPanel.SetActive(true);
        ResultText.text = "Вы Выиграли!";
    }

}
