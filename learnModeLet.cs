using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class learnModeLet : MonoBehaviour
{

 

   // public GameObject letterData;
  //  public DBCon DB;

    public GameObject masterObj;
    MasterCont master;

    public GameObject MIUA;
    MasterIntelUnitAlpha masterInt;


   // public GameObject canvAs;
    public TextMeshProUGUI letterDisp;
    public TextMeshProUGUI katta;
    public TextMeshProUGUI defin;
    public TextMeshProUGUI tyNote;
    public GameObject learnModeItems;
    public TextMeshProUGUI whatShows1;
    public TextMeshProUGUI whatShows2;

    public GameObject wordObj;
    words wordList;

    //  public UserD userC;


    //   public List<AlphaB> learnList;
    bool loadNext = false;
    bool kat = false;
    bool updateAfter = false;
    public bool testModeReady = false;
    public string learnModeS = "kata";



    //test mode data
    public int count = 0;
    static AudioSource sound;
    static AudioClip clips;
    public string pathF;

    public GameObject nextBut;
    public  GameObject backBut;
    public GameObject soundBut;
    public GameObject backGroundLearn;




    public void tog(bool doThis)
    {
        //learn mode text stuff
        learnModeItems.SetActive(doThis);
      //  nextBut.SetActive(doThis);
       // backBut.SetActive(doThis);
       // soundBut.SetActive(doThis);
        backGroundLearn.SetActive(doThis);

    }

    public void loadData()
    {




    }

    public void primeData()
    {
    
      
        master = masterObj.GetComponent<MasterCont>();
        testModeReady = true;
        masterInt = MIUA.GetComponent<MasterIntelUnitAlpha>();
        wordList = wordObj.GetComponent<words>();
        

    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    void finished()
    {

    }

    public void playSound()
    {
        sound = gameObject.AddComponent<AudioSource>();
      
        StartCoroutine(loadAudioFile());
    }

    private IEnumerator loadAudioFile()
    {
        WWW request = getAudFile(pathF);
        yield return request;
        clips = request.GetAudioClip();
       
        playFinal();
    }

    void playFinal()
    {
        sound.clip = clips;
        sound.pitch = 0.9f;
        sound.Play();
   

    }

    private WWW getAudFile(string full)
    {
        string audLoad = full;
        WWW request = new WWW(audLoad);

        return request;
    }

    bool dontPlay = false;
    
    //method to display text on screen and play sound
    public void displayNextLet(string stage)
    {

        try
        {
            switch (stage)
            {
                case "first":
                    //count should be 0
                    learnModeS = masterInt.currentMode;

                    break;
                case "next":

                    bool check = masterInt.learnMode.Count - 1 == count;
                    Debug.Log("next: " + masterInt.learnMode.Count + " vs " + count + "triggers " + check);
                    if (masterInt.learnMode.Count - 1 == count)
                    {
                        dontPlay = true;
                        count = 0;
                        master.startTest();
                        //learnList.Clear();
                        break;
                    }
                    count++;
                    master.playStrum();
                    break;
                case "back":
                    if (count == 0 || count == -1) { }
                    else
                    {
                        count--;

                    }

                    break;

            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        bool android = true;
        AlphaB targ = masterInt.learnMode[count];
       
        string soundSt = "file://" + Application.streamingAssetsPath + "/Hira/" + targ.rom.Trim() + ".wav";
        if (master.isThisMobile)
        {
            soundSt = "jar:file://" + Application.dataPath + "!/assets/" + "Hira/" + targ.rom.Trim() + ".wav";
        }
        pathF = soundSt;



        if (learnModeS.Equals("hira"))
        {
            letterDisp.SetText(targ.hira);
            katta.SetText(targ.kata);
            defin.SetText(targ.rom);
            tyNote.SetText(targ.ty);
            whatShows1.SetText(wordList.hiragaW);
            whatShows2.SetText(wordList.KataW);
        }
        else if (learnModeS.Equals("kata"))
        {
            Debug.Log("Kata Display normal mode");
            letterDisp.SetText(targ.kata);
            katta.SetText(targ.hira);
            defin.SetText(targ.rom);
            tyNote.SetText(targ.ty);
            whatShows1.SetText(wordList.KataW);
            whatShows2.SetText(wordList.hiragaW);
        }
        else
        {
            if (targ.userHira < targ.userKat)
            {
                letterDisp.SetText(targ.kata);
                katta.SetText(targ.hira);
                defin.SetText("");
                tyNote.SetText("");
                whatShows1.SetText(wordList.KataW);
                whatShows2.SetText(wordList.hiragaW);

            }
            else
            {
                letterDisp.SetText(targ.hira);
                katta.SetText(targ.kata);
                defin.SetText("");
                tyNote.SetText("");
                whatShows1.SetText(wordList.hiragaW);
                whatShows2.SetText(wordList.KataW);
            }
        }


        if (dontPlay == false)
        {
            playSound();
        }
      
        

    }

    //loads data based off progress of user
    public void startLearn()
    {
        dontPlay = false;
       

        displayNextLet("first");
    }







}
