using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine.PostProcessing;


public class MasterCont : MonoBehaviour {

    public bool isThisMobile = true;
    public bool firstload = true;

    public GameObject wordObj;
    words worldList;

    public GameObject testObject;
    testMode testMode;
    public GameObject MIUA;
    MasterIntelUnitAlpha MastIntUA;

    public GameObject menu;
    public GameObject startObj;
    public StartBut startB;

    public GameObject settingsObj;
    public TextMeshProUGUI graphTxt;
    public TextMeshProUGUI currentGraphTxt;
    public TextMeshProUGUI difTxt;
    public TextMeshProUGUI currentDifTxt;

    public Camera mainCam;
    public GameObject mobilePos;
    public GameObject pcPos;
    PostProcessingProfile currentPPP;
    public PostProcessingProfile[] pppList;

    public GameObject XMLObj;
    public XmlManager xml;

    public settingPrimary masterSettings;
    public userP masterUser;

    public GameObject storeObj;

    public List<AlphaB> masterLetters = new List<AlphaB>();

    public GameObject backGround;

    //getName info
    public GameObject getName;

    //learnMode
    public GameObject learnModeObj;
    learnModeLet learn;

    //game sounds
    public AudioClip[] strums;
    AudioSource sound;

    public AudioClip[] rif;

    public AudioClip win;
    public AudioClip wrong;
    public AudioClip tick;


    bool backShow = false;
    public TextMeshProUGUI backGroundTXT;
    public TextMeshProUGUI backGroundButTXT;

    public bool camMobile = true;
    public bool updateCamFOV = false;
    public float mobileF = 24f;
    public float pcFloat = 64f;


    public GameObject startOBJ;
    public GameObject startMobileSpot;
    public GameObject startPCSpot;
    public GameObject scoreOBJ;
    public GameObject scoreMobileSpot;
    public GameObject scorePCSpot;
    public GameObject wrongOBJ;
    public GameObject wrongMobileSpot;
    public GameObject wrongPCSpot;
    public GameObject settingsOBJ;
    public GameObject settingsMobileSpot;
    public GameObject settingsPCSpot;
    public GameObject storeOBJ;
    public GameObject storeMobileSpot;
    public GameObject storePCSpot;
    public GameObject menuOBJ;
    public GameObject menuMobileSpot;
    public GameObject menuPCSpot;

    public Canvas menuC;
    public GameObject menuHOBJ;
    public GameObject menuPC;
    public Canvas storeC;
    public GameObject storeHOBJ;
    public GameObject storePC;
    public Canvas settingsC;
    public GameObject settingsHOBJ;
    public GameObject settingsPC;
    public GameObject stuff;
    public GameObject stuffScale;
    public GameObject stuffScale2;
    public GameObject baseScale;

    public TextMeshProUGUI camTxt;
    public TextMeshProUGUI camButTxt;

    public TextMeshProUGUI adminText;

    public GameObject adminSc;

    public bool adminShowB = false;
    public void adminShow()
    {
        if (adminShowB==false)
        {
            adminSc.SetActive(true);
            adminShowB = true;
            updateLetterAdmin();
        }
        else
        {
            adminSc.SetActive(false);
            adminShowB = false;

        }

    }

    public string load = "";
    public void updateLetterAdmin()
    {
        load = "";

        int countBump = 0; 
        foreach (AlphaB a in masterLetters)
        {
            load += "ID: "+a.iD + "Hira " + a.hira + " hira points: " + a.userHira + " kata: " + a.kata+ " kata points: " + a.userKat +" ";
            if (countBump == 1)
            {
                load += " \n";
                countBump = 0;
            }
            else
            {
                countBump++;
            }
            
          //  Debug.Log(load);
        }
        adminText.SetText(load);
       // return load;
    }

    public void addFan()
    {
        masterUser.stars = masterUser.stars + 10;

    }
    public void increaseLetters()
    {
        MastIntUA.cheat();
    }

    public void freeMode()
    {

        masterUser.lastLetterH = 104;
        masterUser.lastLetterK = 104;
    }

    public float testTimeRet()
    {

        if (masterSettings.diffaculty == 0)
        {
            return 10.0f;
        }
        else if (masterSettings.diffaculty == 1)
        {
            return 7.0f;
        }
        else if (masterSettings.diffaculty == 2)
        {
            return 5.0f;
        }
        else
        {
            masterSettings.diffaculty = 0;
            return 10.0f;
        }

       
    }

    public void bumpCamPos()
    {
        mainCam = Camera.main;

        if (camMobile!=true)
        {
            moveToMobile();
            masterSettings.camera = 2;
         //   camMobile = false;
        }
        else
        {
            moveToPC();
            masterSettings.camera = 1;
            //  camMobile = true;



        }
        saveSettings();
        updateCamFOV = true;
    }

    public void checkCamVer()
    {
        if (masterSettings.camera == 0)
        {
            if (isThisMobile)
            {
                moveToMobile();
                masterSettings.camera = 2;
                camMobile = true;
                firstload = false;

            }
            else
            {
                moveToPC();
                masterSettings.camera = 1;
                camMobile = false;
                firstload = false;

            }

        }
        else if (masterSettings.camera == 1)
        {
            moveToPC();

          //  camMobile = false;

        }
        else if (masterSettings.camera == 2)
        {
            moveToMobile();
           
            
        }
        if (isThisMobile)
        {
            if (masterSettings.texture > 3)
            {
                masterSettings.texture = 3;
            }

        }
    }


    public void moveToPC()
    {
        mainCam.transform.position = pcPos.transform.position;
        mainCam.transform.rotation = pcPos.transform.rotation;
       // mainCam.fieldOfView = pcFloat;

        startOBJ.transform.position = startPCSpot.transform.position;
        startOBJ.transform.rotation = startPCSpot.transform.rotation;

        scoreOBJ.transform.position = scorePCSpot.transform.position;
        scoreOBJ.transform.rotation = scorePCSpot.transform.rotation;

        wrongOBJ.transform.position = wrongPCSpot.transform.position;
        wrongOBJ.transform.rotation = wrongPCSpot.transform.rotation;

        settingsOBJ.transform.position = settingsPCSpot.transform.position;
        settingsOBJ.transform.rotation = settingsPCSpot.transform.rotation;

        storeOBJ.transform.position = storePCSpot.transform.position;
        storeOBJ.transform.rotation = storePCSpot.transform.rotation;

        menuOBJ.transform.position = menuPCSpot.transform.position;
        menuOBJ.transform.rotation = menuPCSpot.transform.rotation;
        Debug.Log("Moved to PC");
        camMobile = false;
        updateCamFOV = true;

        // menuC.renderMode = RenderMode.WorldSpace;
        //  menuC.transform.position = menuPC.transform.position;
        menuHOBJ.transform.rotation = storePC.transform.rotation;
        menuHOBJ.transform.localScale = storePC.transform.localScale;

        //   storeC.renderMode = RenderMode.WorldSpace;
        //storeC.transform.position = storePC.transform.position;
        storeHOBJ.transform.rotation = storePC.transform.rotation;
        storeHOBJ.transform.localScale = storePC.transform.localScale;

        //  settingsC.renderMode = RenderMode.WorldSpace;
        //settingsC.transform.position = settingsPC.transform.position;
        settingsC.transform.localScale = storePC.transform.localScale;
        
        settingsHOBJ.transform.rotation = storePC.transform.rotation;
        settingsHOBJ.transform.localScale = storePC.transform.localScale;

     //   stuff.transform.localScale = stuffScale.transform.localScale;
        settingsC.worldCamera = mainCam;
     
      //  Application.LoadLevel("mobile");
    }

    public void moveToMobile()
    {
        camMobile = true;
       // Debug.Log("Moved to Mobile");
        mainCam.transform.position = mobilePos.transform.position;
        mainCam.transform.rotation = mobilePos.transform.rotation;
       // mainCam.fieldOfView = mobileF;
       


        startOBJ.transform.position = startMobileSpot.transform.position;
        startOBJ.transform.rotation = startMobileSpot.transform.rotation;

        scoreOBJ.transform.position = scoreMobileSpot.transform.position;
        scoreOBJ.transform.rotation = scoreMobileSpot.transform.rotation;

        wrongOBJ.transform.position = wrongMobileSpot.transform.position;
        wrongOBJ.transform.rotation = wrongMobileSpot.transform.rotation;

        settingsOBJ.transform.position = settingsMobileSpot.transform.position;
        settingsOBJ.transform.rotation = settingsMobileSpot.transform.rotation;

        storeOBJ.transform.position = storeMobileSpot.transform.position;
        storeOBJ.transform.rotation = storeMobileSpot.transform.rotation;

        //  menuC.transform.position = menuPC.transform.position;
        menuHOBJ.transform.rotation = baseScale.transform.rotation;
        menuHOBJ.transform.localScale = baseScale.transform.localScale;

        //   storeC.renderMode = RenderMode.WorldSpace;
        //storeC.transform.position = baseScale.transform.position;
        storeHOBJ.transform.rotation = baseScale.transform.rotation;
        storeHOBJ.transform.localScale = baseScale.transform.localScale;

        //  settingsC.renderMode = RenderMode.WorldSpace;
        //settingsC.transform.position = settingsPC.transform.position;
        settingsHOBJ.transform.rotation = baseScale.transform.rotation;
        settingsHOBJ.transform.localScale = baseScale.transform.localScale;


        //  settingsC.renderMode = RenderMode.ScreenSpaceOverlay;
        //  storeC.renderMode = RenderMode.ScreenSpaceOverlay;
        //  menuC.renderMode = RenderMode.ScreenSpaceOverlay;
        menuOBJ.transform.position = menuMobileSpot.transform.position;
        menuOBJ.transform.rotation = menuMobileSpot.transform.rotation;


        settingsC.transform.localScale = stuffScale2.transform.localScale;
        stuff.transform.localScale = stuffScale2.transform.localScale;
       updateCamFOV = true;
    }

    public void loadLang()
    {
        if (masterSettings.language.Equals(""))
        {
            masterSettings.language = worldList.currentLang;
        }
            worldList.primeLangStr(masterSettings.language);
        worldList.updateText();
        
        selectedLang = masterSettings.language;
        setLangCounter();
        Debug.Log("Loaded lang: " + selectedLang);
    }

    public TextMeshProUGUI langTxt;
    public TextMeshProUGUI langButTxt;

   public  int langCounter = 0;
    string selectedLang = "";
    public void bumpLang()
    {
        langCounter++;
        if (langCounter >= worldList.langs.Count)
        {
            langCounter = 0;
        }

        selectedLang = worldList.langs[langCounter];
        langTxt.SetText(selectedLang);
        if (selectedLang.Equals(masterSettings.language))
        {
        }
        else
        {
            masterSettings.language = selectedLang;

            worldList.primeLangStr(selectedLang);
            worldList.updateText();
        }
    }

    public void setLangCounter()
    {

        for (int i = 0; i < worldList.langs.Count; i++)
        {
            if (masterSettings.language.Equals(worldList.langs[i]))
            {
                langCounter = i;
                Debug.Log("Language selected" + worldList.langs[i]);

            }

        }
    }


    public void togBack()
    {
        if (backShow)
        {
            masterSettings.enviorment = 0;
            backGround.SetActive(false);
            backShow = false;
            backGroundButTXT.SetText("Off");
        }
        else
        { 
            backGround.SetActive(true);
            backShow = true;
            backGroundButTXT.SetText("On");
            masterSettings.enviorment = 1;
        }

    }

    public void save()
    {
        xml.syncDB(masterLetters);
        xml.saveAlpha();
        xml.saveUser(masterUser);
        xml.saveSetings(masterSettings);
    }
    public void saveUser()
    {
        xml.saveUser(masterUser);
    }


    public void deleteSave()
    {
        foreach (AlphaB a in masterLetters)
        {
            a.resetA();

        }
        xml.syncDB(masterLetters);
        xml.saveAlpha();
        userP nU = new userP();
        xml.saveUser(nU);
        masterLetters.Clear();
        settingPrimary newSet = new settingPrimary();
        xml.saveSetings(newSet);

    }

    public bool storeShowing = true;
    public void showHideStore()
    {
        if (storeShowing)
        {
            storeObj.SetActive(false);
            storeShowing = false;
        }
        else
        {
            storeObj.SetActive(true);
            storeShowing = true;
        }
    
    }



   public void startGame()
    {
        backGround.SetActive(false);
       // menu.SetActive(false);
        getName.SetActive(false);
        settingsObj.SetActive(false);
        adminSc.SetActive(false);
        adminShowB = false;
        fireUpFiles();
        //   mobile.enabled = false;
        //   pc.enabled = true;
       

        loadData();
        checkForNewGame();
        
        Debug.Log("Data Loaded");
        Debug.Log("User Loaded: "+ masterUser.nameU);

        // currentPPP = GetComponent<PostProcessingBehaviour>().profile;



        learn.tog(false);
        testMode.hideQuestion(true);
        //  var qualityLevel = QualitySettings.GetQualityLevel();
       
        loadLang();
        worldList.primeLangList();
        checkCamVer();
        toggleGraphics();
        if (masterSettings.enviorment > 0)
        {
            backGround.SetActive(true);
        }


    }



    public void toggleGraphics()
    {

        if (masterSettings.texture == 0)
        {
            QualitySettings.SetQualityLevel(0, true);
          //  QualitySettings.antiAliasing = 2;
            QualitySettings.masterTextureLimit = 3;
            mainCam.GetComponent<PostProcessingBehaviour>().profile = pppList[0];
          //  reloadLevel();
        }
        else if (masterSettings.texture == 1)
        {
            QualitySettings.SetQualityLevel(1, true);
            QualitySettings.antiAliasing = 2;
            QualitySettings.masterTextureLimit = 1;
            mainCam.GetComponent<PostProcessingBehaviour>().profile = pppList[0];
          //  reloadLevel();
        }
        else if (masterSettings.texture == 2)
        {
            QualitySettings.SetQualityLevel(2, true);
            QualitySettings.antiAliasing = 2;
            QualitySettings.masterTextureLimit = 0;
            mainCam.GetComponent<PostProcessingBehaviour>().profile = pppList[0];
          //  reloadLevel();
        }
        else if (masterSettings.texture == 3)
        {
            QualitySettings.SetQualityLevel(3, true);
            QualitySettings.antiAliasing = 3;
            QualitySettings.masterTextureLimit = 0;


            if (camMobile)
            {
                mainCam.GetComponent<PostProcessingBehaviour>().profile = pppList[0];
            }
            else
            {
                mainCam.GetComponent<PostProcessingBehaviour>().profile = pppList[1];
            }
          //  reloadLevel();

        }
        else if (masterSettings.texture == 4)
        {
            QualitySettings.SetQualityLevel(4, true);
            QualitySettings.antiAliasing = 4;
            QualitySettings.masterTextureLimit = 0;
            mainCam.GetComponent<PostProcessingBehaviour>().profile = pppList[2];
           // reloadLevel();
        }
        else if (masterSettings.texture == 5)
        {
            QualitySettings.SetQualityLevel(5, true);
           // QualitySettings.antiAliasing = 4;
            QualitySettings.masterTextureLimit = 0;
            mainCam.GetComponent<PostProcessingBehaviour>().profile = pppList[3];
          //  reloadLevel();
        }
        else
        {
            QualitySettings.SetQualityLevel(0, true);
            mainCam.GetComponent<PostProcessingBehaviour>().profile = pppList[0];
           // reloadLevel();
        }


 

    }

    public void reloadLevel()
    {
        save();
        
        Application.LoadLevel("mobile");


        
    }
    bool changeGraphics = false;
    bool changeNow = false;

    public void bumpGraphics()
    {
        masterSettings.texture += 1;
        if (masterSettings.texture > 5)
        {
            masterSettings.texture = 0;
        }
        if (isThisMobile && masterSettings.texture>3)
        {
            masterSettings.texture = 0;
        }
        changeGraphics = true;
    }

   public bool settingsShow = false;

    public void closeSettings()
    {
        if (settingsShow == false)
        {
            settingsObj.SetActive(true);
            settingsShow = true;
        }
        else
        {
            settingsShow = false;
            settingsObj.SetActive(false);
           // toggleGraphics();
            saveSettings();
          
        }
        if (changeGraphics)
        {
            changeNow = true;
        }

    }

    public void bumpDif()
    {
        masterSettings.diffaculty += 1;
        if (masterSettings.diffaculty>2)
        {
            masterSettings.diffaculty = 0;
        }
    
    }





    //audio scripts
    public void playStrum()
    {
        sound = gameObject.AddComponent<AudioSource>();
    
        int strum = UnityEngine.Random.Range(0, 3);
        sound.clip = strums[strum];
        sound.volume = .2f;
        sound.Play();
    }

    public void playRif()
    {
        sound = gameObject.AddComponent<AudioSource>();
    
        int riff = UnityEngine.Random.Range(0, 3);
        sound.clip = rif[riff];

        sound.Play();

    }

    public void playWin()
    {
        sound = gameObject.AddComponent<AudioSource>();

        sound.clip = win;
        sound.volume = 0.4f;
        sound.Play();
       

    }

    public void playTick()
    {
        sound = gameObject.AddComponent<AudioSource>();

        sound.clip = tick;
        sound.volume = 0.4f;
        sound.Play();


    }

    public void playWrong()
    {
        sound = gameObject.AddComponent<AudioSource>();

        sound.clip = wrong;
        sound.volume = 0.4f;
        sound.Play();
       

    }



    private IEnumerator loadAudioFile()
    {
        
        playfinal();
        return null;
    }

    void playfinal()
    {
        sound.Play();
    }


    int playCounter = 0;

    public void saveAndStartLearn()
    {
        if (playCounter >= 2)
        {
         
            playCounter = 0;
        }
        playCounter++;
        testMode.setTime("");
      
        MastIntUA.startLearnAI();

        xml.saveUser(masterUser);
        xml.syncDB(masterLetters);
        xml.saveAlpha();
      


    }


    public void startLearn(bool stop)
    {
        if (stop == false)
        {
            MastIntUA.startLearnAI();
        }
        else
        {
            learn.tog(false);
        }
    }
    public void stopTest()
    {
        testMode.unspawn();
        testMode.testModeMaster = false;
       
    }

    public void startTest()
    {
        
        learn.tog(false);
        MastIntUA.startTestModeAI();

    }

    public void setCurrentUser(string a)
    {
        masterUser.nameU = a;
        getName.SetActive(false);
        xml.saveUser(masterUser);
    }

    private void checkForNewGame()
    {
        if (masterUser.nameU.Equals("null"))
        {
            getName.SetActive(true);

        }
    }

    public void saveSettings()
    {
        xml.saveSetings(masterSettings);
    }

    void loadData()
    {
  
        masterSettings = xml.loadSettings();
        masterUser = xml.loadUser();
        xml.laodAlpha();
        masterLetters = xml.listLet;
        Debug.Log("Letters loaded: " + masterLetters.Count);

        testMode.primeData();
        learn.primeData();
        MastIntUA.primeData();



    }

    void fireUpFiles()
    {

        testMode = testObject.GetComponent<testMode>();
        learn = learnModeObj.GetComponent<learnModeLet>();
        MastIntUA = MIUA.GetComponent<MasterIntelUnitAlpha>();

        startB = startObj.GetComponent<StartBut>();
        xml = XMLObj.GetComponent<XmlManager>();
        worldList = wordObj.GetComponent<words>();
    }
  

    public bool isShowing = true;
    public void togleMen()
    {
        if (isShowing)
        {
            isShowing = false;
            menu.SetActive(false);
        
        }
        else
        {
            isShowing = true;
            menu.SetActive(true);
        }

    }

    public void startMe()
    {
        togleMen();

    }
    public void qGame()
    {
        Application.Quit();
    }





    // Use this for initialization
    void Start ()
    {
      //  startGame();

    }

    // Update is called once per frame

    private void Awake()
    {
        startGame();
      
    }

    public void updateTextSettings() {
        if (masterSettings.texture == 0)
        {
            currentGraphTxt.SetText(worldList.fast);
        }
        else if (masterSettings.texture == 1)
        {
            currentGraphTxt.SetText(worldList.lowMed);
        }
        else if (masterSettings.texture == 2)
        {
            currentGraphTxt.SetText(worldList.medium);
        }
        else if (masterSettings.texture == 3)
        {

            currentGraphTxt.SetText(worldList.improved);
        }
        else if (masterSettings.texture == 4)
        {
            currentGraphTxt.SetText(worldList.mediumHigh);
        }
        else if (masterSettings.texture == 5)
        {
            currentGraphTxt.SetText(worldList.max);
        }
        else
        {
            
        }
        if (masterSettings.diffaculty == 0)
        {
            currentDifTxt.SetText(worldList.easy);
        }
        if (masterSettings.diffaculty == 1)
        {
            currentDifTxt.SetText(worldList.normal);
        }
        if (masterSettings.diffaculty == 2)
        {
            currentDifTxt.SetText(worldList.hard);
        }
        graphTxt.SetText(worldList.graphics);
        difTxt.SetText(worldList.dif);
        if (masterSettings.enviorment > 0)
        {
            backGroundButTXT.SetText(worldList.on);
        }
        else
        {
            backGroundButTXT.SetText(worldList.off);
        }
        langButTxt.SetText(selectedLang);
        langTxt.SetText(worldList.language);

        camTxt.SetText(worldList.cameraW);
        if (camMobile)
        {
            camButTxt.SetText(worldList.mobile);
        }
        else
        {
            camButTxt.SetText(worldList.landScape);
        }

    }
   
    public TextMeshProUGUI user;

    bool countdownStore = true;
    float normalAspect = 16 / 9f;
    float countdownIn = .4f;
    void Update () {


        if (changeNow && changeGraphics)
        {
            changeGraphics = false;
            changeNow = false;
            reloadLevel();
        } 


            if (countdownStore)
        {
            countdownIn -= Time.deltaTime;
            if (countdownIn <= 0)
            {
                storeObj.SetActive(false);
                countdownStore = false;
                Debug.Log("stopped store");
                loadLang();
                worldList.updateText();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (testMode.testModeMaster)
            {

            }
            else
            {
                togleMen();
            }
           

        }

        try
        {
            user.SetText(masterUser.nameU+"\n"+worldList.scoreW + masterUser.scoreTest +"\n"+ worldList.fansW + masterUser.stars+ "\n"  + worldList.comboW + MastIntUA.currentCurrentCombo) ;
       
        }
        catch (Exception)
        {


        }
       // mainCam.fieldOfView = 64f;

        //updates settings menue
        if (settingsShow)
        {
            updateTextSettings();
        }

        if (updateCamFOV)
        {
            if (camMobile==true)
            {//
              //  mainCam.fieldOfView = mobileF;
                Debug.Log("Changed FOV");
                

                mainCam.fieldOfView = mobileF * normalAspect / ((float)mainCam.pixelWidth / mainCam.pixelHeight);
            }
            else
            {
              //  mainCam.fieldOfView = pcFloat;
                Debug.Log("Changed FOV");

                mainCam.fieldOfView = pcFloat * normalAspect / ((float)mainCam.pixelWidth / mainCam.pixelHeight);
            }
            updateCamFOV = false;
    
        }



    }


  


}
[System.Serializable]
public class letterDataS
{
    public List<AlphaB> mainletters = new List<AlphaB>();

}