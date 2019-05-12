using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;





//this class is to control the learn mode and test mode content. It will determine where the user  was last start learning from that point then provide testing with the learned content. 
//this class is also responsible for adaptive content providing wrong answers back in learning and testing
//from this point on when test mode is turne on it only gets data from here for hirakatafree mode as well what letters. Any data from test mode will be sent back to this class
public class MasterIntelUnitAlpha : MonoBehaviour
{
    //data sources
    public GameObject conObj;
    MasterCont mastC;
   // public GameObject userObj;

    public GameObject learnObj;
    learnModeLet learn;
    public GameObject testModeObj;
    testMode test;
   // public GameObject letterObj;
  //  DBCon letters;

    public GameObject fan;
    public GameObject spawnFan;
    public fanMover fanMo ;


    public string currentMode = "";
    int lastLetter = 0;
    bool lastAnswerRight = false;


    //combo data
    public int currentCurrentCombo = 0;


    //UI code data
    public TextMeshProUGUI listA;

    public void updateList()
    {
        string combine = "";
        foreach (AlphaB a in learnMode)
        {
            combine = combine +a.hira;

        }
        listA.SetText(combine);
    }

    public void primeData()
    {
      
        learn = learnObj.GetComponent<learnModeLet>();
        test = testModeObj.GetComponent<testMode>();
       // letters = letterObj.GetComponent<DBCon>();
        mastC = conObj.GetComponent<MasterCont>();
       // localLetters = letters.getLet();
    }

    public  List<AlphaB> localLetters;
    public  List<AlphaB> learnMode = new List<AlphaB>();
    static List<AlphaB> wrongAnswer = new List<AlphaB>();
    static List<AlphaB> freeMode = new List<AlphaB>();



    //Learn Mode code
    public void startLearnAI()
    {
        localLetters = mastC.masterLetters;

        learnMode = new List<AlphaB>();
        //sets string for mode based off hiragana learning
        currentMode = determineMode();

       
        //fills learn mode based off mode and user data
        fillLearn();
        checkForWrongGiven();
        Debug.Log("Starting learn mode");
        startLearnMode();
    }

    private void checkForWrongGiven()
    {
        if (wrongAnswer.Count > 0)
        {
            foreach (AlphaB a in wrongAnswer)
            {
                learnMode.Add(a);
            }
            wrongAnswer.Clear();
        }
    }

    private void startLearnMode()
    {
        learn.tog(true);
        learn.startLearn();
    }

    //updates last letter after completed test
    int lastLetBuff = 0;
    public void lastLetterUser()
    {

        if (currentMode.Equals("hira"))
        {
            mastC.masterUser.lastLetterH = lastLetBuff;
        }
        else if (currentMode.Equals("kata"))
        {
            mastC.masterUser.lastLetterK = lastLetBuff;
        }


    }

    //obtains letters for training
    private void fillLearn()
    {
       
        if (currentMode.Equals("hira"))
        {
            //first game found if this triggers
            if (mastC.masterUser.lastLetterH < 6)
            {
                Debug.Log("First game letters:");
                pullLetters(0, 6);
                // mastC.masterUser.lastLetterH = 6;
                lastLetBuff = 6;

            }
            else if (mastC.masterUser.lastLetterH >= 6 && mastC.masterUser.lastLetterH < 98)
            {
                Debug.Log("Old last letter : " + mastC.masterUser.lastLetterH);
                pullLetters(mastC.masterUser.lastLetterH, mastC.masterUser.lastLetterH + 6);

                lastLetBuff = mastC.masterUser.lastLetterH + 6;
                //mastC.masterUser.lastLetterH +=6;
                Debug.Log("New last letter is : " + mastC.masterUser.lastLetterH);
            }
            else
            {
                Debug.Log("final pool");
                pullLetters(mastC.masterUser.lastLetterH, 104);
                mastC.masterUser.lastLetterH = 104;
               
            }


        }
        else if (currentMode.Equals("kata"))
        {
            if (mastC.masterUser.lastLetterK < 6)
            {
                Debug.Log("First game letters:");
                pullLetters(0, 6);
                // mastC.masterUser.lastLetterK = 6;
                lastLetBuff = 6;
            }
            else if (mastC.masterUser.lastLetterK >= 6 && mastC.masterUser.lastLetterK < 98)
            {
                Debug.Log("Old last letter : " + mastC.masterUser.lastLetterK);
                pullLetters(mastC.masterUser.lastLetterK, mastC.masterUser.lastLetterK + 6);
                // user.lastU.LastLetterK += 6;
                lastLetBuff = mastC.masterUser.lastLetterK + 6;
                Debug.Log("New last letter is : " + mastC.masterUser.lastLetterK);
            }
            else
            {
                pullLetters(mastC.masterUser.lastLetterK, 104);
                mastC.masterUser.lastLetterK = 104;
            }
        }
        else
        {
            //free mode learning start
            Debug.Log("free mode starting");
            foreach (AlphaB b in localLetters)
            {
                b.setPriority();
            }
            freeMode.Clear();
            foreach (AlphaB a in localLetters)
            {
                freeMode.Add(a);
            }
            sortLearnList();
            for (int add = 0; add < 6; add++)
            {
                learnMode.Add(freeMode[add]);
            }

        }
    }

    //sorts list based off priority. Priority is determined by the history of the character
    void sortLearnList()
    {

        AlphaB temp;
        for (int b = 0; b< freeMode.Count-1;b++)
        {
            for (int i = 0; i < freeMode.Count-1; i++)
            {
                if (freeMode[i].priority < freeMode[i+1].priority)
                {
                    temp = freeMode[i+1];

                    freeMode[i + 1] = freeMode[i];

                    freeMode[i] = temp;

                }
            }

        }
        string counter = "List order = ";
        foreach (AlphaB b in freeMode)
        {
            counter += b.priority + " , ";

        }

        Debug.Log(counter);

    }


    //generic pull letters from List 
    void pullLetters(int a, int b)
    {
        Debug.Log(a + " " + b);

        try
        {
            for (int i = a; i < b; i++)
            {

                Debug.Log(localLetters[i].hira);
                learnMode.Add(localLetters[i]);

            }
        }
        catch (Exception E)
        {
            Debug.Log(E);
        }

    }
  public  bool cheatf = true;


    //cheat
    public void cheat()
    {
        //mastC.masterUser.lastLetterH = 104;
        //mastC.masterUser.lastLetterK = 104;
      
        if (true)
        {
           // mastC.masterUser.stars = 100;
            // mastC.masterUser.lastLetterH = 74;
            //  mastC.masterUser.lastLetterH = 0;
            foreach (AlphaB a in localLetters)
              {
              a.userHira = 2;
                 a.userKat = 2;
             }
         //   cheatf = false;
        }
    }


    private string determineMode()
    {
    //  cheat();
        lastLetter = 0;
        string modef = "";
            int hira = mastC.masterUser.lastLetterH;
            Debug.Log("last Letter H" + hira);
            if (hira != 104)
            {
                modef = "hira";
                lastLetter = hira;
            //lastLetter = mastC.masterUser.lastLetterH;
            Debug.Log("Hira mode Triggered!");
        }
            else if (mastC.masterUser.lastLetterK != 104)
            {
                modef = "kata";
                lastLetter = mastC.masterUser.lastLetterK;
            Debug.Log("Kata mode Triggered!");
            }
            else
            {
                modef = "free";
            Debug.Log("Free mode Triggered!");
        }
            return modef;
        
    }


    //end of learn mode Code
   public GameObject[] pop;
    

    //test code this will mostly react to incoming data
    public void popCombo()
    {
        fanMo = fan.GetComponent<fanMover>();
      

        pop[0]  = Instantiate(fan, spawnFan.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

       
       // fanMo.boom();
    }


    //test mode start determines mode
    public void startTestModeAI()
    {
       
     

        if (lastLetter>30)
        {
            addRando(lastLetter);
        }
        Debug.Log("test mode AI picked up");
        startTestMode();

    }


    //sets last letter after completed testing
    private void updateLastLetter()
    {
        int high = -1;

        foreach (AlphaB al in learnMode)
        {
            if (al.iD > high)
            {
                high = al.iD;
            }
        }

        if (currentMode.Equals("hira"))
        {
            if (mastC.masterUser.lastLetterH < high)
            {
                mastC.masterUser.lastLetterH = high;
            }


        }
        else if (currentMode.Equals("kata"))
        {
            if (mastC.masterUser.lastLetterK < high)
            {
                mastC.masterUser.lastLetterK = high;
            }

        }
    }

    //starts test sets master to test to prevent esc from opening 
    private void startTestMode()
    {
        Debug.Log("Test mode started with: "+ learnMode.Count);
        Debug.Log("Test mode started with: " + currentMode);
        test.testModeMaster = true;
        test.StartTestMode(currentMode);
    }

    public void wrongAnsGiven(AlphaB wrong)
    {
        wrongAnswer.Add(wrong);
    }

    //adds random letters that were already tested on
    void addRando(int i)
    {
        try
        {
            var r = UnityEngine.Random.Range(0, i);
            var r2 = UnityEngine.Random.Range(0, i);
            var r3 = UnityEngine.Random.Range(0, i);

            learnMode.Add(localLetters[r]);
            learnMode.Add(localLetters[r2]);
            learnMode.Add(localLetters[r3]);

        }
        catch (Exception e) { }

    }
   

    //updates activity for letter adds wrong or right ticks 
  public void upDateLetter(bool rightA, AlphaB isRight, bool currentHira)
    {



        if (rightA)
        {
            if (currentMode.Equals("hira"))
            {
                if (isRight.wrongHira > 0)
                {
                    isRight.wrongHira--;
                }


                    isRight.userHira++;
                


            }
            else if (currentMode.Equals("kata"))
            {
                if (isRight.wrongKat > 0)
                {
                    isRight.wrongKat--;
                }
                
                    isRight.userKat++;
                

            }
            else
            {
                if (currentHira)
                {
                    if (isRight.wrongHira > 0)
                    {
                        isRight.wrongHira--;
                    }


                    isRight.userHira++;



                }
                else
                {
                    if (isRight.wrongKat > 0)
                    {
                        isRight.wrongKat--;
                    }

                    isRight.userKat++;

                }
            }


        }
        else
        {
            if (currentMode.Equals("hira"))
            {

                isRight.wrongHira++;

            }
            else if (currentMode.Equals("kata"))
            {
                isRight.wrongKat++;

            }
            else
            {
                if (currentHira)
                {
                    isRight.wrongHira++;
                }
                else
                {
                    isRight.wrongHira++;
                }
            }
        }


        letterCheck(isRight);
    }
    public GameObject spawn;
    //checks for mastery
    void letterCheck(AlphaB isRight)
    {
        if (isRight.userHira == 3 && isRight.mastHirBool == 0)
        {
            isRight.userHira++;
            isRight.mastHirBool = 1;
            test.pauseT(true);
        }
        if (isRight.userKat == 3 && isRight.mastKatBool == 0)
        {
            isRight.userKat++;
            isRight.mastKatBool = 1;
            //todo code prompt for proficient message *grats*
        }


    }

    //adds combo points
    void comboUp()
    {
        lastAnswerRight = true;
        currentCurrentCombo++;
     
        if (mastC.masterUser.maxCombo < currentCurrentCombo)
        {
            mastC.masterUser.maxCombo = currentCurrentCombo;
        }
        if (currentCurrentCombo > 5)
        {
            mastC.masterUser.stars++;
            spawnFan = spawn;
            popCombo();
        }
    }

    public void comboHit(bool answer)
    {
        if (answer && lastAnswerRight || answer && currentCurrentCombo == 0 )
        {
            comboUp();
        }
        else
        {
            lastAnswerRight = false;
            currentCurrentCombo = 0;
        }


    }

    //end of test mode code





}

