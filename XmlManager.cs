using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;

public class XmlManager : MonoBehaviour
{

   

    public void saveUser(userP nSet)
    {
        try
        {
            string theFile = "/userP.xml";
            FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.Create); ;


            XmlSerializer serializer = new XmlSerializer(typeof(userP));


            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, nSet);
            stream.Close();
        } catch 
        (Exception e)
        {
            string theFile = "/userP.xml";
            FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.CreateNew); ;


            XmlSerializer serializer = new XmlSerializer(typeof(userP));


            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, nSet);
            stream.Close();
        }
        Debug.Log("User with name of: " + nSet.nameU);

    }

    public userP loadUser()
    {
        userP nSet = new userP();
        string theFile = "/userP.xml";

        try
        {
            bool two = File.Exists((Application.persistentDataPath + theFile));
            if (two)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(userP));
                FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.Open);
                nSet = serializer.Deserialize(stream) as userP;
                Debug.Log("loading User with name of: " + nSet.nameU);
                stream.Close();
            }
            else
            {
                saveUser(nSet);
            }

        }
        catch 
        (Exception e)
        {
            saveUser(nSet);
        }
  



     
      

        return nSet;
    }



    ///
    ///Settings code
    ///
    public void saveSetings(settingPrimary nSet)

    {
        try
        {
            string theFile = "/settings.xml";
            // settingPrimary nSet = new settingPrimary();
            XmlSerializer serializer = new XmlSerializer(typeof(settingPrimary));

            FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.Create);
            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, nSet);
            stream.Close();
        }
        catch (Exception e)
        {
            string theFile = "/settings.xml";
            // settingPrimary nSet = new settingPrimary();
            XmlSerializer serializer = new XmlSerializer(typeof(settingPrimary));

            FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.CreateNew);
            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, nSet);
            stream.Close();
        }
  

    }

    public settingPrimary loadSettings()
    {
        settingPrimary nSet = new settingPrimary();

        string theFile = "/settings.xml";
        try
        {
            bool two = File.Exists((Application.persistentDataPath + theFile));


            if (two)
            {

                XmlSerializer serializer = new XmlSerializer(typeof(settingPrimary));

                FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.Open);
                nSet = serializer.Deserialize(stream) as settingPrimary;
                stream.Close();
            }
            else
            {
                saveSetings(nSet);
            }


        }
        catch (Exception e)
        {
            saveSetings(nSet);
        }
     
        
        return nSet;
    }


    /// <summary>
    /// alphabet code
    /// </summary>
    public List<AlphaB> listLet = new List<AlphaB>();
    public List<AlphaB> listLetCheck = new List<AlphaB>();

    public static XmlManager ins;
    private void Awake()
    {
        ins = this;
    }

    public alphaDataH apDB;

    public void syncDB(List<AlphaB> a)
    {
        apDB.list.Clear();
        foreach (AlphaB Ap in a)
        {
            alphaLetter newLet = new alphaLetter();
            newLet.iD = Ap.iD;
            newLet.hira = Ap.hira;
            newLet.kata = Ap.kata;
            newLet.rom = Ap.rom;
            newLet.ty = Ap.ty;
            newLet.userHira = Ap.userHira;
            newLet.userKat = Ap.userKat;
            newLet.userAdv = Ap.userAdv;
            newLet.proHiraBool = Ap.proHiraBool;
            newLet.prokatBool = Ap.prokatBool;
            newLet.mastHirBool = Ap.mastHirBool;
            newLet.mastKatBool = Ap.mastKatBool;
            newLet.wrongHira = Ap.wrongHira;
            newLet.wrongKat = Ap.wrongKat;
            newLet.priority = Ap.priority;

            // Debug.Log("Letter: " + newLet.iD);
            apDB.list.Add(newLet);

        }
    }

    public void saveAlpha()
    {
        string theFile = "/alphaDB.xml";
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<alphaLetter>));

            FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.Create);

            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, apDB.list);
            stream.Close();
        }
        catch (Exception e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<alphaLetter>));

            FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.CreateNew);

            var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, apDB.list);
            stream.Close();
        }
     
    }

    public void laodAlpha()
    {
        primeCheck();
        try
        {
            string theFile = "/alphaDB.xml";

            bool two = File.Exists((Application.persistentDataPath + theFile));


            if (two)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<alphaLetter>));
                FileStream stream = new FileStream(Application.persistentDataPath + theFile, FileMode.Open);
                var streamWriter = new StreamReader(stream, System.Text.Encoding.UTF8);

                apDB.list = serializer.Deserialize(stream) as List<alphaLetter>;

                stream.Close();


            }
            else
            {
                primeLetters();


            }
        }
        catch (Exception)
        {
            primeLetters();
        }
    
        

        Debug.Log("Alpha load from XML = "+ apDB.list.Count);

        foreach (alphaLetter a in apDB.list)
        {
             AlphaB alLoad = new AlphaB();
          
            alLoad.iD = a.iD;
            alLoad.hira = a.hira;
            alLoad.kata = a.kata;
            alLoad.rom = a.rom;
            alLoad.ty = a.ty;
            alLoad.userHira = a.userHira;
            alLoad.userKat = a.userKat;
            alLoad.userAdv = a.userAdv;
            alLoad.proHiraBool = a.proHiraBool;
            alLoad.mastHirBool = a.mastHirBool;
            alLoad.prokatBool = a.prokatBool;
            alLoad.wrongHira = a.wrongHira;
            alLoad.wrongKat = a.wrongKat;
            alLoad.priority = a.priority;



            alLoad = dataCheck(alLoad);
            listLet.Add(alLoad);
        }
    }
    public AlphaB dataCheck(AlphaB loc)
    {
        foreach (AlphaB a in listLetCheck)
        {
            if (a.iD == loc.iD)
            {
                if (a.rom.Equals(loc.rom))
                { }
                else
                {
                    loc.rom = a.rom;
                }
            }
        }

        return loc;
    }

    public void primeLetters()
    {
        AlphaB alLoad;
        int id = 1;


        listLet.Add(alLoad = new AlphaB(id++, "ア", "あ", "a"));
        listLet.Add(alLoad = new AlphaB(id++, "イ", "い", "i"));
        listLet.Add(alLoad = new AlphaB(id++, "ウ", "う", "u"));
        listLet.Add(alLoad = new AlphaB(id++, "エ", "え", "e"));
        listLet.Add(alLoad = new AlphaB(id++, "オ", "お", "o"));
        listLet.Add(alLoad = new AlphaB(id++, "カ", "か", "ka"));
        listLet.Add(alLoad = new AlphaB(id++, "キ", "き", "ki"));
        listLet.Add(alLoad = new AlphaB(id++, "ク", "く", "ku"));
        listLet.Add(alLoad = new AlphaB(id++, "ケ", "け", "ke"));
        listLet.Add(alLoad = new AlphaB(id++, "コ", "こ", "ko"));
        listLet.Add(alLoad = new AlphaB(id++, "サ", "さ", "sa"));
        listLet.Add(alLoad = new AlphaB(id++, "シ", "し", "shi"));
        listLet.Add(alLoad = new AlphaB(id++, "ス", "す", "su"));
        listLet.Add(alLoad = new AlphaB(id++, "セ", "せ", "se"));
        listLet.Add(alLoad = new AlphaB(id++, "ソ", "そ", "so"));
        listLet.Add(alLoad = new AlphaB(id++, "タ", "た", "ta"));
        listLet.Add(alLoad = new AlphaB(id++, "チ", "ち", "chi"));
        listLet.Add(alLoad = new AlphaB(id++, "ツ", "つ", "tsu"));
        listLet.Add(alLoad = new AlphaB(id++, "テ", "て", "te"));
        listLet.Add(alLoad = new AlphaB(id++, "ト", "と", "to"));
        listLet.Add(alLoad = new AlphaB(id++, "ナ", "な", "na"));
        listLet.Add(alLoad = new AlphaB(id++, "ニ", "に", "ni"));
        listLet.Add(alLoad = new AlphaB(id++, "ヌ", "ぬ", "nu"));
        listLet.Add(alLoad = new AlphaB(id++, "ネ", "ね", "ne"));
        listLet.Add(alLoad = new AlphaB(id++, "ノ", "の", "no"));
        listLet.Add(alLoad = new AlphaB(id++, "ハ", "は", "ha"));
        listLet.Add(alLoad = new AlphaB(id++, "ヒ", "ひ", "hi"));
        listLet.Add(alLoad = new AlphaB(id++, "フ", "ふ", "fu"));
        listLet.Add(alLoad = new AlphaB(id++, "ヘ", "へ", "he"));
        listLet.Add(alLoad = new AlphaB(id++, "ホ", "ほ", "ho"));
        listLet.Add(alLoad = new AlphaB(id++, "マ", "ま", "ma"));
        listLet.Add(alLoad = new AlphaB(id++, "ミ", "み", "mi"));
        listLet.Add(alLoad = new AlphaB(id++, "ム", "む", "mu"));
        listLet.Add(alLoad = new AlphaB(id++, "メ", "め", "me"));
        listLet.Add(alLoad = new AlphaB(id++, "モ", "も", "mo"));
        listLet.Add(alLoad = new AlphaB(id++, "ヤ", "や", "ya"));
        listLet.Add(alLoad = new AlphaB(id++, "ユ", "ゆ", "yu"));
        listLet.Add(alLoad = new AlphaB(id++, "ヨ", "よ", "yo"));
        listLet.Add(alLoad = new AlphaB(id++, "ラ", "ら", "ra"));
        listLet.Add(alLoad = new AlphaB(id++, "リ", "り", "ri"));
        listLet.Add(alLoad = new AlphaB(id++, "ル", "る", "ru"));
        listLet.Add(alLoad = new AlphaB(id++, "レ", "れ", "re"));
        listLet.Add(alLoad = new AlphaB(id++, "ロ", "ろ", "ro"));
        listLet.Add(alLoad = new AlphaB(id++, "ワ", "わ", "wa"));
        listLet.Add(alLoad = new AlphaB(id++, "ヲ", "を", "o"));
        listLet.Add(alLoad = new AlphaB(id++, "ン", "ん", "n"));
        listLet.Add(alLoad = new AlphaB(id++, "ガ", "が", "ga"));
        listLet.Add(alLoad = new AlphaB(id++, "ギ", "ぎ", "gi"));
        listLet.Add(alLoad = new AlphaB(id++, "グ", "ぐ", "gu"));
        listLet.Add(alLoad = new AlphaB(id++, "ゲ", "げ", "ge"));
        listLet.Add(alLoad = new AlphaB(id++, "ゴ", "ご", "go"));
        listLet.Add(alLoad = new AlphaB(id++, "ザ", "ざ", "za"));
        listLet.Add(alLoad = new AlphaB(id++, "ジ", "じ", "ji"));
        listLet.Add(alLoad = new AlphaB(id++, "ズ", "ず", "zu"));
        listLet.Add(alLoad = new AlphaB(id++, "ゼ", "ぜ", "ze"));
        listLet.Add(alLoad = new AlphaB(id++, "ゾ", "ぞ", "zo"));
        listLet.Add(alLoad = new AlphaB(id++, "ダ", "だ", "da"));
        listLet.Add(alLoad = new AlphaB(id++, "ヂ", "ぢ", "di"));
        listLet.Add(alLoad = new AlphaB(id++, "ヅ", "づ", "zu"));
        listLet.Add(alLoad = new AlphaB(id++, "デ", "で", "de"));
        listLet.Add(alLoad = new AlphaB(id++, "ド", "ど", "do"));
        listLet.Add(alLoad = new AlphaB(id++, "バ", "ば", "ba"));
        listLet.Add(alLoad = new AlphaB(id++, "ビ", "び", "bi"));
        listLet.Add(alLoad = new AlphaB(id++, "ブ", "ぶ", "bu"));
        listLet.Add(alLoad = new AlphaB(id++, "ベ", "べ", "be"));
        listLet.Add(alLoad = new AlphaB(id++, "ボ", "ぼ", "bo"));
        listLet.Add(alLoad = new AlphaB(id++, "パ", "ぱ", "pa"));
        listLet.Add(alLoad = new AlphaB(id++, "ピ", "ぴ", "pi"));
        listLet.Add(alLoad = new AlphaB(id++, "プ", "ぷ", "pu"));
        listLet.Add(alLoad = new AlphaB(id++, "ペ", "ぺ", "pe"));
        listLet.Add(alLoad = new AlphaB(id++, "ポ", "ぽ", "po"));
        listLet.Add(alLoad = new AlphaB(id++, "キャ", "きゃ", "kya"));
        listLet.Add(alLoad = new AlphaB(id++, "キュ", "きゅ", "kyu"));
        listLet.Add(alLoad = new AlphaB(id++, "キョ", "きょ", "kyo"));
        listLet.Add(alLoad = new AlphaB(id++, "シャ", "しゃ", "sha"));
        listLet.Add(alLoad = new AlphaB(id++, "シュ", "しゅ", "shu"));
        listLet.Add(alLoad = new AlphaB(id++, "ショ", "しょ", "sho"));
        listLet.Add(alLoad = new AlphaB(id++, "チャ", "ちゃ", "cha"));
        listLet.Add(alLoad = new AlphaB(id++, "チュ", "ちゅ", "chu"));
        listLet.Add(alLoad = new AlphaB(id++, "チョ", "ちょ", "cho"));
        listLet.Add(alLoad = new AlphaB(id++, "ニャ", "にゃ", "nya"));
        listLet.Add(alLoad = new AlphaB(id++, "ニュ", "にゅ", "nyu"));
        listLet.Add(alLoad = new AlphaB(id++, "ニョ", "にょ", "nyo"));
        listLet.Add(alLoad = new AlphaB(id++, "ヒャ", "ひゃ", "hya"));
        listLet.Add(alLoad = new AlphaB(id++, "ヒュ", "ひゅ", "hyu"));
        listLet.Add(alLoad = new AlphaB(id++, "ヒョ", "ひょ", "hyo"));
        listLet.Add(alLoad = new AlphaB(id++, "ミャ", "みゃ", "mya"));
        listLet.Add(alLoad = new AlphaB(id++, "ミュ", "みゅ", "myu"));
        listLet.Add(alLoad = new AlphaB(id++, "ミョ", "みょ", "myo"));
        listLet.Add(alLoad = new AlphaB(id++, "リャ", "りゃ", "rya"));
        listLet.Add(alLoad = new AlphaB(id++, "リュ", "りゅ", "ryu"));
        listLet.Add(alLoad = new AlphaB(id++, "リョ", "りょ", "ryo"));
        listLet.Add(alLoad = new AlphaB(id++, "ギャ", "ぎゃ", "gya"));
        listLet.Add(alLoad = new AlphaB(id++, "ギュ", "ぎゅ", "gyu"));
        listLet.Add(alLoad = new AlphaB(id++, "ギョ", "ぎょ", "gyo"));
        listLet.Add(alLoad = new AlphaB(id++, "ジャ", "じゃ", "jya"));
        listLet.Add(alLoad = new AlphaB(id++, "ジュ", "じゅ", "jyu"));
        listLet.Add(alLoad = new AlphaB(id++, "ジョ", "じょ", "jyo"));
        listLet.Add(alLoad = new AlphaB(id++, "ビャ", "びゃ", "bya"));
        listLet.Add(alLoad = new AlphaB(id++, "ビュ", "びゅ", "byu"));
        listLet.Add(alLoad = new AlphaB(id++, "ビョ", "びょ", "byo"));
        listLet.Add(alLoad = new AlphaB(id++, "ピャ", "ぴゃ", "pya"));
        listLet.Add(alLoad = new AlphaB(id++, "ピュ", "ぴゅ", "pyu"));
        listLet.Add(alLoad = new AlphaB(id++, "ピョ", "ぴょ", "pyo"));





    }

    void primeCheck()
    {
        AlphaB alLoad;
        int id = 1;


        listLetCheck.Add(alLoad = new AlphaB(id++, "ア", "あ", "a"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "イ", "い", "i"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ウ", "う", "u"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "エ", "え", "e"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "オ", "お", "o"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "カ", "か", "ka"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "キ", "き", "ki"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ク", "く", "ku"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ケ", "け", "ke"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "コ", "こ", "ko"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "サ", "さ", "sa"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "シ", "し", "shi"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ス", "す", "su"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "セ", "せ", "se"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ソ", "そ", "so"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "タ", "た", "ta"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "チ", "ち", "chi"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ツ", "つ", "tsu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "テ", "て", "te"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ト", "と", "to"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ナ", "な", "na"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ニ", "に", "ni"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヌ", "ぬ", "nu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ネ", "ね", "ne"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ノ", "の", "no"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ハ", "は", "ha"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヒ", "ひ", "hi"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "フ", "ふ", "fu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヘ", "へ", "he"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ホ", "ほ", "ho"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "マ", "ま", "ma"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ミ", "み", "mi"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ム", "む", "mu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "メ", "め", "me"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "モ", "も", "mo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヤ", "や", "ya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ユ", "ゆ", "yu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヨ", "よ", "yo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ラ", "ら", "ra"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "リ", "り", "ri"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ル", "る", "ru"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "レ", "れ", "re"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ロ", "ろ", "ro"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ワ", "わ", "wa"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヲ", "を", "o"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ン", "ん", "n"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ガ", "が", "ga"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ギ", "ぎ", "gi"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "グ", "ぐ", "gu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ゲ", "げ", "ge"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ゴ", "ご", "go"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ザ", "ざ", "za"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ジ", "じ", "ji"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ズ", "ず", "zu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ゼ", "ぜ", "ze"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ゾ", "ぞ", "zo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ダ", "だ", "da"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヂ", "ぢ", "di"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヅ", "づ", "zu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "デ", "で", "de"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ド", "ど", "do"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "バ", "ば", "ba"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ビ", "び", "bi"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ブ", "ぶ", "bu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ベ", "べ", "be"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ボ", "ぼ", "bo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "パ", "ぱ", "pa"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ピ", "ぴ", "pi"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "プ", "ぷ", "pu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ペ", "ぺ", "pe"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ポ", "ぽ", "po"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "キャ", "きゃ", "kya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "キュ", "きゅ", "kyu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "キョ", "きょ", "kyo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "シャ", "しゃ", "sha"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "シュ", "しゅ", "shu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ショ", "しょ", "sho"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "チャ", "ちゃ", "cha"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "チュ", "ちゅ", "chu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "チョ", "ちょ", "cho"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ニャ", "にゃ", "nya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ニュ", "にゅ", "nyu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ニョ", "にょ", "nyo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヒャ", "ひゃ", "hya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヒュ", "ひゅ", "hyu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ヒョ", "ひょ", "hyo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ミャ", "みゃ", "mya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ミュ", "みゅ", "myu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ミョ", "みょ", "myo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "リャ", "りゃ", "rya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "リュ", "りゅ", "ryu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "リョ", "りょ", "ryo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ギャ", "ぎゃ", "gya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ギュ", "ぎゅ", "gyu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ギョ", "ぎょ", "gyo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ジャ", "じゃ", "jya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ジュ", "じゅ", "jyu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ジョ", "じょ", "jyo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ビャ", "びゃ", "bya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ビュ", "びゅ", "byu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ビョ", "びょ", "byo"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ピャ", "ぴゃ", "pya"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ピュ", "ぴゅ", "pyu"));
        listLetCheck.Add(alLoad = new AlphaB(id++, "ピョ", "ぴょ", "pyo"));



    }
}



[System.Serializable]
public class alphaLetter
    {

  
    public int iD=0;
    public string hira = "";
    public string kata = "";
    public string rom = "";
    public string ty = "";
    public int userHira = 0;
    public int userKat = 0;
    public int userAdv = 0;
    public int proHiraBool = 0;
    public int prokatBool = 0;
    public int mastHirBool = 0;
    public int mastKatBool = 0;
    public int wrongHira = 0;
    public int wrongKat = 0;
    public int priority = 0;

    public alphaLetter() { }



}

[System.Serializable]
public class alphaDataH
{
   public  alphaDataH()
    {

    }

    public List<alphaLetter> list = new List<alphaLetter>();
   


}

[System.Serializable]
public class settingPrimary
{


    public int iDSet = 0;
    public int lastUser = 0;
    public int diffaculty = 0;
    public int enviorment = 0;
    public int board = 0;
    public int dice = 0;
    public int boardT = 0;
    public int diceT = 0;
    public int volume = 50;
    public int texture = 0;
    public int camera = 0;
    public string language ="";

    public List<int> playerBoards = new List<int>();
    public List<int> playerCubes = new List<int>();
    public string timeOfDay = "night";

    public settingPrimary()
        {
  
        }

  }

[System.Serializable]
public class userP
{
    public int iD;
    public string nameU = "null";
    public string namae = "null";
    public int lettersProficient = 0;
    public int wordsProficient = 0;
    public int lettersMastered = 0;
    public int wordsMastered = 0;
    public int scoreTest = 0;
    public int lastLetterH = 0;
    public int lastLetterK = 0;
    public int lastWord = 0;
    public int backGround = 0;
    public int font = 0;
    public int board = 0;
    public int dice = 0;
    public int boardTrim = 0;
    public int diceTrim = 0;
    public int maxCombo = 0;
    public int stars = 0;
    public int difficulty = 0;

    public userP()
    {

    }


}