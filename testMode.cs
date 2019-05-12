using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class testMode : MonoBehaviour {
    public GameObject masterObj;
    MasterCont master;

    public GameObject learnModeObj;
    learnModeLet learn;

    //public GameObject letDBObj;
   // public DBCon DB;
    public List<AlphaB> send;
    //public spawnAns spawnAns;
    public Transform[] ansLoc;
    public GameObject[] spawnClone;
    public GameObject Prefabs;
    //die text?
    public TextMeshProUGUI[] textMeshM;
    //question text
    public TextMeshProUGUI questPrefab;

    public GameObject MIUA;
    public MasterIntelUnitAlpha mastInt;

    public GameObject textObjselect;
    bool runOnce = false;
    public int learnCount = 6;
    public int testCount = 6;
    public int masterNum ;
    int testCountsession = 0;
    public string testTarget;
    static  List<AlphaB> lastTest = new List<AlphaB>();
   // static List<AlphaB> testSession = new List<AlphaB>();
    static  List<AlphaB> buff = new List<AlphaB>();
    int target;
    public int wrongAnswer = 0;
    public bool rightAnswer = false;
    bool dataLoad = false;
    public bool testModeRun = false;

    public int hits = 0;

    //public GameObject backgroundTest;



    public GameObject messageObj;
    public TextMeshProUGUI message;
    public animMessage mesAn;

    public GameObject wrong1Obj;
    public GameObject wrong2Obj;
    public TextMeshProUGUI wrong1;
    public TextMeshProUGUI wrong2;

    //time for spawn
    public float interval = 1f;
    public float timeLeft = .15f;
    bool isSpawning = false;
    public float ansInter = 3.5f;
    public float spawnAnsTime = 3.5f;
    bool answesGiven = false;
    bool testing = false;
    public float testTime = 10f;

    //primary switch for modes in testing
    string finalMode;

    //this int tracks how many times the test has been run through
    public int stageT = 0;

    //alpha current right answer
    public AlphaB isRight;

    public void hideQuestion(bool yes)
    {
        if (yes)
        {
          // backgroundTest.SetActive(false);
            questPrefab.SetText(" ");
        }
        else
        {
          //  backgroundTest.SetActive(true);
        }
    }

    public string answerCorrect = "";

    private string iAmMe;

    public string IAmMe
    {
        get
        {
            return iAmMe;
        }

        set
        {
            iAmMe = value;
        }
    }

    public void primeData()
    {
    
        master = masterObj.GetComponent<MasterCont>();
        mastInt = MIUA.GetComponent<MasterIntelUnitAlpha>();
        learn = learnModeObj.GetComponent<learnModeLet>();
        mesAn = messageObj.GetComponent<animMessage>();
        messageObj.SetActive(false);
        wrong1Obj.SetActive(false);
        wrong2Obj.SetActive(false);

    }

    public GameObject buttonSource;
    private clickCube scriptBut;
    //sends data to cubes when created
    void sendData(AlphaB cube)
    {

        scriptBut = buttonSource.GetComponent<clickCube>();
        scriptBut.ID = cube.iD;
        scriptBut.targCheck = testTarget;
        scriptBut.thisCubeis = cube;

    }

    public void sayName(string path)
    {
        learn.pathF = path;
        learn.playSound();
    }



    public void StartTestMode( string modeGiven)
    {

       

         Debug.Log("test count: "+ mastInt.learnMode.Count);

        shuffleA(mastInt.learnMode);
        finalMode = modeGiven;
        modeSpawn();



    }

    public void endTest()
    {
        unspawn();
        setTime("");
        stageT = 0;
        testModeMaster = false;
        master.saveAndStartLearn();
        
        
    }
    bool currentHira = true;

    //loop for test start
    void modeSpawn()
    {
         testTime= master.testTimeRet();
        if (mastInt.learnMode.Count > stageT)
        {
          //  Debug.Log(finalMode+" Has been started");
            switch (finalMode)
            {
                case "hira":
                    answesGiven = false;
                     
                    answerCorrect = mastInt.learnMode[stageT].rom;
                    
                  
                    isRight = mastInt.learnMode[stageT];
             ;
                    giveSpawnOrder();
                    createListofWrong();
                
                    addRightToWronglist();
                    shuffleA(buff);
              

                    spawnStart();
                    break;
                case "kata":
                    answerCorrect = mastInt.learnMode[stageT].hira;
                   
                    isRight = mastInt.learnMode[stageT];
                    giveSpawnOrder();
                    createListofWrong();
                  
                    addRightToWronglist();
                    shuffleA(buff);
                    

                    spawnStart();
                    break;
                case "free":
                    if (mastInt.learnMode[stageT].userHira >= mastInt.learnMode[stageT].userKat)
                    {
                        answerCorrect = mastInt.learnMode[stageT].kata;
                        currentHira = false;
                    }
                    else
                    {
                        answerCorrect = mastInt.learnMode[stageT].hira;
                        currentHira = true;
                    }
                    isRight = mastInt.learnMode[stageT];
                    giveSpawnOrder();
                    createListofWrong();
                    addRightToWronglist();
                    shuffleA(buff);
                   
                   spawnStart();

                    break;
            }

            stageT++;

        }
        else
        {
            Debug.Log("test end data saved");
            mastInt.lastLetterUser();
            endTestB = true;
        }
    }


    //list of wrong all of these have been shown before
    void createListofWrong()
    {
        buff.Clear();
        for (int i = 0; i < 11; i++)
        {
            if (finalMode.Equals("hira"))
            {
                buff.Add(getNotAnswer(master.masterUser.lastLetterH ));
            } else if (finalMode.Equals("kata"))
            {
                buff.Add(getNotAnswer(master.masterUser.lastLetterK));
            }
            else
            { buff.Add(getNotAnswer(104)); }
        }

    }

    //adds the correct answer so there is always a right cube
    void addRightToWronglist()
    {
        buff.Add(isRight);
    }



    //used with createlistofwrong this will find letters that are not the current letter
    AlphaB getNotAnswer( int lastLet)
    {
        AlphaB tmp = new AlphaB(1, "der", "der", "der");
        var r = UnityEngine.Random.Range(0, lastLet);


        if (lastLet < 30)
        {
            //   Debug.Log("early learning selection");
            r = UnityEngine.Random.Range(0, lastLet + 30);
        }

        tmp = master.masterLetters[r];
        if (tmp.iD == isRight.iD)
        {
            tmp = getNotAnswer(lastLet);
        }
        if (tmp.iD == 45 && isRight.iD == 5|| tmp.iD == 5 && isRight.iD==45)
        {
            tmp = getNotAnswer(lastLet);
        }

        return tmp;
    }


    





  
    //sets text for question
    void spawnQuestionMeth(string q)
    {
        questPrefab.SetText(q);


    }

    //shuffles string array
    void shuffleA(List<AlphaB> vs)
    {
       
        for (var i = vs.Count - 1; i > 0; i--)
        {
            var r = UnityEngine.Random.Range(0, i);
            var tmp = vs[i];
            vs[i] = vs[r];
            vs[r] = tmp;
        }
    }
    void shuffleA(List<int> vs)
    {

        for (var i = vs.Count - 1; i > 0; i--)
        {
            var r = UnityEngine.Random.Range(0, i);
            var tmp = vs[i];
            vs[i] = vs[r];
            vs[r] = tmp;
        }
    }


    //sends a string and int to  spawn to give letter and position
    public void starTest1(List<AlphaB> a, AlphaB target)
    {
       
        foreach (AlphaB letC in a)
        {
           
            currentCount++;
        }
    }
    //spawns a cube with a letter and positions it in the board with the int
   
    void spawnAnsMeth(string l, int p,AlphaB cube)
    {
       // Debug.Log("String: "+ l+" Int: "+ p);

       
        try
        {
                IAmMe = l;

          // scriptBut.thisCubeis = cube;
           textMeshM[0].text = l;
           textMeshM[1].text = l;
           textMeshM[2].text = l;
           textMeshM[3].text = l;
           textMeshM[4].text = l;
           textMeshM[5].text = l;
                sendData(cube);
            if(p<=20)
            {
                spawnClone[p] = Instantiate(Prefabs, ansLoc[p].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            }
                

              
            }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.Log(e);
        }

        currentCount++;

    }

//correct answer submited
    void testCorrect()
    {
        rightAnswer = false;
       
        unspawn();

  
    }
//unspawn items
   public void unspawn()
    {
        testing = false;
        spawnQuestionMeth(" ");
        setTime("");

        foreach (GameObject spawnAns in spawnClone)
        {
            Destroy(spawnAns);

        }

    }
//wrong answer
    void testFail()
    {
        unspawn();
        
    }

    // Use this for initialization
    void Start () {
       

    }

    //adds wrong
    void addWrong()
    {
        if (finalMode.Equals("hira"))
        {
            isRight.wrongHira ++;
        }
        else if (finalMode.Equals("kata"))
        {
            isRight.wrongKat ++;
        }
        for (int i = 0; i < lastTest.Count; i++)
        {
            if (lastTest[i].iD == isRight.iD)
            {

            } else if (lastTest.Count==i)
            {
                lastTest.Add(isRight);
            }
        }
    }


    //spawn cube meths based off time
    void spawnStart()
    {
        isSpawning = true;
       // hideQuestion(true);
    }
    void spawnStop()
    {
       
       // hideQuestion(false);
        isSpawning = false;
       // answesGiven = true;
    }

    public static int currentCount = 0;

    //popcube is a spawn method for creating a cube timed event uses this to spawn the cubes slowly 
    void popCube()
    {
        int properCount = buff.Count - 1;
      //  Debug.Log("current Count: "+currentCount + "Cubes to spawn:" + properCount);
        if (currentCount > buff.Count-1)
        {
            spawnStop();
            currentCount = 0;
        }
        else
        {
            if (finalMode.Equals("hira"))
            {
                if (isRight.mastHirBool == 1)
                {
                    spawnAnsMeth(buff[currentCount].kata, spawnOrder[currentCount], buff[currentCount]);
                }
                else
                {
                    spawnAnsMeth(buff[currentCount].rom, spawnOrder[currentCount], buff[currentCount]);
                }

               
            }
            else if (finalMode.Equals("kata"))
            {
                if (isRight.mastKatBool == 1 || isRight.userAdv>2)
                {
                    spawnAnsMeth(buff[currentCount].hira, currentCount, buff[currentCount]);
                }
                else
                {
                    spawnAnsMeth(buff[currentCount].rom, currentCount, buff[currentCount]);
                }

                
            }
            else if (finalMode.Equals("free"))
            {
                if (currentHira)
                {
                  //  Debug.Log("Free mode Kata answer spawned");
                    spawnAnsMeth(buff[currentCount].kata, spawnOrder[currentCount], buff[currentCount]);
                }
                else
                {
                  //  Debug.Log("Free mode hira answer spawned");
                    spawnAnsMeth(buff[currentCount].hira, currentCount, buff[currentCount]);
                }
            }

        }

        
        
    }
   // bool currentQuestionisHira = false;

        // after most of the letters have hit hte ground this is called to show the question
    void giveQuest()
    {
        if (canShow)
        {
            if (finalMode.Equals("hira"))
            {
                if (isRight.mastHirBool == 1)
                {
                    spawnQuestionMeth(isRight.hira);
                }
                else
                {
                    spawnQuestionMeth(isRight.hira);
                }

            }
            else if (finalMode.Equals("kata"))
            {
                spawnQuestionMeth(isRight.kata);
            }
            else
            {
                if (currentHira)
                {
                    // Debug.Log("Hira Question given");
                    spawnQuestionMeth(isRight.hira);
                }
                else
                {
                    // Debug.Log("Kata Question given: "+ isRight.kata);
                    spawnQuestionMeth(isRight.kata);
                }
            }

        }
       

     
     answesGiven = true;

    }
    List<int> spawnOrder = new List<int>();
    List<int> spawnOrder1 = new List<int>();

    //mixes up the spawns to create a different spawn animation every time(mostly)
    void giveSpawnOrder()
    {
        spawnOrder.Clear();
        spawnOrder1.Clear();
        for (int i = 0; i < 6; i++)
        {
            spawnOrder.Add(i);
          //  Debug.Log("thinking of: " + i);
        }

        for (int b = 6; b < 12; b++)
        {
            spawnOrder1.Add(b);
           // Debug.Log("thinking of: " + b);
        }



        shuffleA(spawnOrder);
        shuffleA(spawnOrder1);
        foreach (int a in spawnOrder1)
        {
            spawnOrder.Add(a);
        }

   


    }


    //resets timers and bools
    private void dataReset()
    {
        wrongAnswer = 0;
        runOnce = false;
        testing = false;
     //   testTime = 10f;
        tickTarg = 10;
        soundTick = 10;
        spawnAnsTime = interval;
        hits = 0;
        answesGiven = false;
        // canShow = false;
        wrong1Obj.SetActive(false);
        wrong2Obj.SetActive(false);

    }


    //checks mastery of letter
    void checkIfadv()
    {
        Debug.Log("Checking Mastery "+ isRight.userHira);
        if (isRight.userHira == 3)
        {
            Debug.Log("is master");
            pauseT(true);
            message.SetText(isRight.hira+" = "+isRight.kata);

        }
    }

    //for pausing test to alert of mastery 
    public void pauseT(bool yes)
    {
        if (yes)
        {
            testModeMaster = false;
            messageObj.SetActive(true);
           
            master.playRif();
            message.SetText(isRight.hira + " = " + isRight.kata);
            mesAn.messageGo();
        }
        else
        {
            testModeMaster = true;
            messageObj.SetActive(false);
        }
    }

    // the trigger
    public void unPaseT()
    {
        pauseT(false);

    }

    //wrong answer
    void failTest()
    {
        dataReset();
        mastInt.comboHit(false);
        mastInt.upDateLetter(false, isRight, currentHira);
        setTime("");
        testFail();
        // Debug.Log("Game Over");
       
        master.playWrong();
       
        setTime("");
        if (stageT > mastInt.learnMode.Count) { endTest(); } else
        {
            Debug.Log("Letter Updated");
            mastInt.lastLetterUser();
            modeSpawn();
        }

    }
    //sound for wrong
   public void playWrong()
    {
        master.playWrong();
    }

    public GameObject spawn;

    //correct answer
    void passTest()
    {
        mastInt.spawn = spawn;
        mastInt.comboHit(true);
        mastInt.upDateLetter(true, isRight, currentHira);
        setTime("");
        master.masterUser.scoreTest++;
        // Debug.Log("you win!");
        dataReset();
        testCorrect();
        master.playWin();
      // checkIfadv();
        setTime("");
        if (stageT > mastInt.learnMode.Count) { endTest(); } else
        {
            Debug.Log("Letter Updated");
            mastInt.lastLetterUser();
            modeSpawn();
        }
    }
    //count down code
    int tickTarg = 10;
    bool tickAgain = false;
    bool hasTicked = false;
    int soundTick = 10;

    public TextMeshProUGUI countDowntxt;
    //10 second count down
    void timeCheck(int time)
    {
        if (time ==9)
        {
            setTime("十");
        }
        else if (time ==8)
        {
            setTime("九");
        }
        else if (time ==7)
        {
            setTime("八");
        }
        else if (time ==6)
        {
            setTime("七");
        }
        else if (time ==5)
        {
            setTime("六");
        }
        else if (time ==4)
        {
            setTime("五");
        }
        else if (time == 3)
        {
            setTime("四");
        }
        else if (time == 2)
        {
            setTime("三");
        }
        else if (time == 1)
        {
            setTime("二");
        }
        else if (time == 0)
        {
            setTime("一");
        }
        else if (time <0)
        {
            setTime("!");
        }

    }


    // if wrong it will show cube that was wrong
    public void wrongDisp(string a, int b)
    {
        Debug.Log("Display wrong: " + a + " " + b);
        if (b == 1)
        {
            wrong1Obj.SetActive(true);
            wrong1.SetText(a);
        }
        else if (b == 2)
        {
            wrong2Obj.SetActive(true);
            wrong2.SetText(a);
        }
        else
        {
            wrong1Obj.SetActive(false);
            wrong2Obj.SetActive(false);

        }
    

      
}

    //sets count down text 
    public void setTime(string t)
    {

        countDowntxt.SetText(t);
    }


    public bool testModeMaster = false;

    public bool wrongAnswerGiven = false;
    float testEnd = .6f;
    bool endTestB = false;
    //buff time for test fail
    bool bufftime = false;
    bool canShow = true;
    float buffT = 3f;
    public string wrongAn = "";

    // Update is called once per frame
    void Update () {
        if (testModeMaster)
        {
            // Debug.Log("test update");
            if (endTestB)
            {
                testEnd -= Time.deltaTime;
                if (testEnd < 0)
                {
                    endTestB = false;
                    testEnd = .6f;
                    endTest();

                }

            }

            if (wrongAnswerGiven)
            {
                wrongDisp(wrongAn, wrongAnswer);
                addWrong();
                wrongAnswerGiven = false;
            }

            if (wrongAnswer >= 3)
            {
                failTest();
            }
            if (rightAnswer)
            {
                passTest();
            }


            if (bufftime)
            {
                buffT -= Time.deltaTime;
                if (buffT < 0)
                {
                    bufftime = false;
                    buffT = 1f;
                    canShow = true;

                }

            }

            //spawn timer
            if (isSpawning)
            {

                timeLeft -= Time.deltaTime;


                if (timeLeft < 0)
                {
                    timeLeft = interval;
                    popCube();
                }

            }

            //old bool answesGiven
            //checks spawn time then shows question
            if (hits > 3)
            {
                testing = false;
                spawnAnsTime -= Time.deltaTime;
                if (spawnAnsTime < 0 || hits >= 11)
                {
                    if (canShow)
                    {
                        hits = 0;
                        spawnAnsTime = interval;
                        giveQuest();
                        testing = true;
                       // answesGiven = false;
                       // canShow = false;
                        bufftime = true;

                    }

                }
            }


            //test mode count down
            if (testing && canShow)
            {
                testTime -= Time.deltaTime;
                if (testTime < tickTarg)
                {
                    tickTarg--;
                }

                if (testTime < 0)
                {

                    failTest();

                }
                timeCheck(tickTarg);
                //playes sound for count down
                if (soundTick != tickTarg)
                {
                    //  Debug.Log("test time: "+testTime+" tickTarg: "+ tickTarg);
                    master.playTick();
                    soundTick = tickTarg;
                }

            }


        }
        else
        {
            unspawn();
        }


    }
}
