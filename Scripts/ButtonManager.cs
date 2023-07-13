using System.Collections;
using System.Collections.Generic;
using Ardunity;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO.Ports;
using UnityEngine.Events;
using System.Runtime.InteropServices;

public enum RobotType {
    ArmRobot,
    PistonRobot,
    Type3,
    Type4,
    Type5,
    Type6,
    Type7,
    Type8,
    Type9,
    Type10
};

//MonoBehaviour
public class ButtonManager : CommSocket
{
    //지정된 창의 표시 상태 설정
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    //활성화된 윈도우-함수를 호출한 쓰레드와 연동 된 녀석의 핸들을 받는다.
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    public Image robotTypeImg;
    public Image menuImg;

    public Transform teleportPos;

    [Header("Robot Type")]
    public GameObject[] robotArms;
    [Header("ArdunityApps")]
    public ArdunityApp[] ardunityApps;
    [Header("CommSerials")]
    public CommSerial[] commSerials;
    [Header("Dropdowns")]
    public Dropdown[] dropdowns;
    [Header("Set Button")]
    public Button setBtn;
    [Header("RobotType Buttons")]
    public Button[] robotTypeBtns;

    private ArdunityApp _selectedArdunityApp;
    private CommSerial _selectedCommSerial;

    void Start()
    {
        for (int i = 0; i < ardunityApps.Length; i++)
        {
            ardunityApps[i].OnConnected.AddListener(OnConnected);
            ardunityApps[i].OnConnectionFailed.AddListener(OnConnectionFailed);
            ardunityApps[i].OnDisconnected.AddListener(OnDisconnected);
            ardunityApps[i].OnLostConnection.AddListener(OnLostConnection);
        }

        for (int i = 0 ; i < robotArms.Length ; i++)
        {
            robotArms[i].SetActive(false);
        }

        // Button 비활성화
        for (int i = 0; i < robotTypeBtns.Length ; i++)
        {
            robotTypeBtns[i].interactable = false;
        }
        
        // 모든 포트 찾아서 드랍목록 넣기
        SetDropdownOptions();
    }

    /// <summary>
    /// 프로그램 종료
    /// </summary>
    public void OnWindowCloseButtonClick()
    {
        Application.Quit();
    }

    /// <summary>
    /// 윈도우폼 최소화 함수
    /// </summary>
    public void OnMinimizeButtonClick()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void OnButton1()
    {
        robotTypeImg.sprite = Resources.Load<Sprite>("RobotType/Type1");
        menuImg.sprite = Resources.Load<Sprite>("RobotType/Menu1");

        // 현재 모델링만 Set true
        OnSetActive(robotArms[0]);

        // 1. 모든 연결 끊기
        AllDisconnection();

        // 2. 포트 세팅
        PortSetting(ref commSerials[0], RobotType.ArmRobot);

        // 3. 현재 설정중인 Ardunity 넣기
        SelectArdunityApp(ardunityApps[0], commSerials[0], RobotType.ArmRobot);

        // 4. 연결
        StartButton(RobotType.ArmRobot);
    }

    public void OnButton2()
    {
        robotTypeImg.sprite = Resources.Load<Sprite>("RobotType/Type2");
        menuImg.sprite = Resources.Load<Sprite>("RobotType/Menu2");

#if DEBUG_PORT
        // Port 생김
        PortSelection.SetActive(true);
#endif
        // 현재 모델링만 Set true
        OnSetActive(robotArms[1]);

        // 1. 모든 연결 끊기
        AllDisconnection();

        // 2. Port를 찾는다( 설정하기 )
        PortSetting(ref commSerials[1], RobotType.PistonRobot);

        // 3. 현재 설정된 Ardunity, CommSerial 넣기
        SelectArdunityApp(ardunityApps[1], commSerials[1], RobotType.PistonRobot);

        // 4. 연결
        StartButton(RobotType.PistonRobot);
    }

    void PortSetting(ref CommSerial _commSerial, RobotType _robotType)
    {
        _commSerial.device.address = "//./" + dropdowns[(int)_robotType].captionText.text;
    }

    void OnSetActive(GameObject _robotArm)
    {
        for (int i = 0; i < robotArms.Length ;i++)
        {
            robotArms[i].SetActive(false);
        }

        _robotArm.SetActive(true);
    }

    void OnConnected()
    {
        Debug.Log("On Connected");
    }

    void OnConnectionFailed()
    {
        Debug.Log("On Connection Failed");
    }

    void OnDisconnected()
    {
        Debug.Log("On Disconnected");
    }

    void OnLostConnection()
    {
        Debug.Log("On Lost Connection");
    }

    void AllDisconnection()
    {
        for (int i = 0; i < ardunityApps.Length ; i++)
        {
            ardunityApps[i].Disconnect();
        }
    }

    private void SetDropdownOptions()// Dropdown 목록 생성
    {
        for (int i = 0; i < dropdowns.Length ;i++)
        {
            dropdowns[i].options.Clear();
        }

        // Port 검색
        string[] ports = SerialPort.GetPortNames();
        Debug.Log("port Length : " + ports.Length);

        // 각각의 dropdown에 포트 다 넣어줌
        for (int i = 0 ; i < dropdowns.Length; i++)
        {
            for (int j = 0; j < ports.Length; j++)//1부터 10까지
            {
                // Caption 대기 고정
                if (j==0)
                {
                    Dropdown.OptionData captionText = new Dropdown.OptionData();
                    captionText.text = "Robot"+(i+1);
                    dropdowns[i].options.Add(captionText);
                }

                Dropdown.OptionData option = new Dropdown.OptionData();
                //option.text = i.ToString() + "갯수";
                Debug.Log("port : " + ports[j]);
                option.text = ports[j];
                dropdowns[i].options.Add(option);
            }
        }
    }

    public void SetDropdownCaptionText()
    {
        // 1. 드랍다운 캡션에 표시 첫번째꺼
        for (int i = 0; i < dropdowns.Length ;i++)
        {
            dropdowns[i].value = 0;
            dropdowns[i].Select();
            dropdowns[i].RefreshShownValue();
        }
    }

    public void SearchButton()// SelectButton을 누름으로써 값 테스트.
    {
        SetDropdownOptions();
        //Debug.Log("Dropdown Value: " + dropdown.value + ", List Selected: " + (dropdown.value + 1));
    }

    public void StartButton(RobotType robotType)
    {
        // dropdown 에 port가 선택 되어있지 않으면 시작하지 않음.
        //Debug.Log("dropdown.captionText.text.Substring(0,4) : " + dropdown.captionText.text);

        if (dropdowns[(int)robotType].captionText.text.Substring(0).Equals("R"))
        {
            Debug.Log("It Doesn't Selected Port!");
            return;
        }

        // 연결
        _selectedArdunityApp.Connect();
    }

    public void SelectArdunityApp(ArdunityApp ardunityApps, CommSerial commSerial, RobotType robotType)
    {
        _selectedArdunityApp = ardunityApps;
        commSerials[(int)robotType] = commSerial;
        Debug.Log("현재 설정중인 Serial 포트의 COM+N : " + commSerials[(int)robotType].device.address);
    }

    public void OnValueChanged(int i)
    {
        try
        {
            commSerials[i].device.address = "//./" + dropdowns[i].captionText.text;

            Debug.Log("dropdowns["+i+"].captionText.text : " + dropdowns[i].captionText.text);

        } catch( Exception e )
        {
            Debug.Log(e);
        }   
    }

    public void OnSetBtn()
    {
        string currentSetBtn = setBtn.GetComponentInChildren<Text>().text;

        Debug.Log("currentSetBtn : " + currentSetBtn);

        if (currentSetBtn.Equals("Set"))
        {
            // 현재 버튼의 상태가 Set 이면 UnSet 으로
            setBtn.GetComponentInChildren<Text>().text = "UnSet";
            
            for (int i = 0; i < dropdowns.Length; i++)
            {
                dropdowns[i].interactable = false;
                robotTypeBtns[i].interactable = true;
            }
        }
        else
        {
            // 아니면 Set
            // Set 일때 드랍다운 비활성화
            setBtn.GetComponentInChildren<Text>().text = "Set";

            for(int i = 0; i < dropdowns.Length ;i++)
            {
                dropdowns[i].interactable = true;
                robotTypeBtns[i].interactable = false;
            }
        }
    }
}

