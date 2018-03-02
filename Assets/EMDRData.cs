using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EMDRData : MonoBehaviour
{
    string hostURL = "http://jiaqiwang.000webhostapp.com/EMDR2018/"; public string HostURL { get { return hostURL; } }
    string filename_UserData = "/userdata.dat"; public string Filename_PlayerInfo { get { return filename_UserData; } }
    string filename_Round = "/round.dat"; public string Filename_Round { get { return filename_Round; } }
    User user; public User User { get { return user; } }
    //Round round; public Round Round { get { return round; } }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator Init()
    {
        yield return StartCoroutine(Coroutine_LoadUser());
    }

    IEnumerator PostForm(string url, WWWForm form)
    {
        var download = UnityWebRequest.Post(url, form);

        // Wait until the download is done
        yield return download.SendWebRequest();

        if (download.isNetworkError || download.isHttpError)
        {
            print("Error downloading: " + download.error);
        }
        else
        {
            // show the page
            Debug.Log(download.downloadHandler.text);
        }
    }

    //User Data
    public void SaveLocal_UserData(UserData newUserData)
    {
        string localUserdataPath = Application.persistentDataPath + filename_UserData;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(localUserdataPath);
        bf.Serialize(file, newUserData);
        file.Close();
    }
    public UserData LoadLocal_UserData()
    {
        UserData ret = new UserData();
        string pdpPlayerInfo = Application.persistentDataPath + filename_UserData;
        if (File.Exists(pdpPlayerInfo))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pdpPlayerInfo, FileMode.Open);
            ret = (UserData)bf.Deserialize(file);
            file.Close();
        }
        return ret;
    }

    //User
    public IEnumerator Coroutine_CreateUser(string firstname)
    {
        //Create data on the server side
        string url = HostURL + "createuser.php";
        WWWForm form = new WWWForm();
        form.AddField("firstname", firstname);
        var download = UnityWebRequest.Post(url, form);
        yield return download.SendWebRequest();
        if (download.isNetworkError || download.isHttpError)
            print("Error downloading: " + download.error);
       
        //Save user data locally for next time use
        int newUserID = 0;
        Int32.TryParse(download.downloadHandler.text, out newUserID);
        SaveLocal_UserData(new UserData(newUserID));
    }
    public IEnumerator Coroutine_UpdateUser(User updatedUser)
    {
        user = updatedUser;

        //Update data on the server side
        string url = HostURL + "updateuser.php";
        WWWForm form = new WWWForm();

        form.AddField("id", LoadLocal_UserData().id);
        form.AddField("firstname", user.firstname);
        form.AddField("lastname", user.lastname);
        form.AddField("email", user.email);
        form.AddField("emailconfirmed", user.emailconfirmed);
        form.AddField("emailconfirmationcode", user.emailconfirmationcode);
        form.AddField("pw", user.pw);
        form.AddField("username", user.username);
        form.AddField("problems", user.problems);
        form.AddField("problemdetails", user.problemdetails);

        // Wait until PostForm is done
        yield return PostForm(url, form);
    }
    IEnumerator Coroutine_LoadUser()
    {
        int id = LoadLocal_UserData().id;
        if (id > 0)
        {
            //Update data on the server side
            string url = HostURL + "selectuserbyid.php";
            WWWForm form = new WWWForm();
            form.AddField("id", id);

            //Download user data from server
            var download = UnityWebRequest.Post(url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                // show the page
                //Debug.Log(download.downloadHandler.text);

                string sqlRow = download.downloadHandler.text;
                user = new User(sqlRow, ',');
                user.Print();
            }
        }
        else
            Debug.Log("No local user data");
    }

    //Round
    public IEnumerator Coroutine_CreateRound()
    {
        yield return new WaitForSeconds(1);
        print("Coroutine_CreateRound unimplemented");
    }
    public IEnumerator Coroutine_UpdateRound()
    {
        yield return new WaitForSeconds(1);
        print("Server update unimplemented");

    }
    IEnumerator Coroutine_LoadRound()
    {
        yield return new WaitForSeconds(1);
        print("Coroutine_LoadRound unimplemented");
    }
    public void SaveLocal_Round(Round newRound)
    {
        newRound.Print();
        //round = newRound;
        string localRoundPath = Application.persistentDataPath + filename_Round;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(localRoundPath);
        bf.Serialize(file, newRound);
        file.Close();
    }
    public Round LoadLocal_Round()
    {
        Round ret = new Round();
        string localRoundPath = Application.persistentDataPath + filename_Round;
        if (File.Exists(localRoundPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(localRoundPath, FileMode.Open);
            ret = (Round)bf.Deserialize(file);
            file.Close();
        }
        return ret;
    }
}

[Serializable]
public class UserData
{
    public int id=0;

    public UserData()
    {
    }

    public UserData(int newID)
    {
        id = newID;
    }
}

//Buffer for user instance 
public class User
{
    public string invalidChars = ", /~*";

    public int id;
    public string firstname = "";
    public string lastname = "";
    public DateTime datetime_registered;
    public string email = "";
    public string emailconfirmed = "0";
    public string emailconfirmationcode = "";
    public string pw = "";
    public string username = "";
    public string problems = "";
    public string problemdetails = "";

    public User() { }

    public User(string fn, string ln, string un, string em)
    {
        firstname = fn;
        lastname = ln;
        username = un;
        email = em;
    }

    public User(string sqlRow, char delimiter)
    {
        string[] cols = sqlRow.Split(delimiter);
        if (cols.Length == 10)
        {
            Int32.TryParse(cols[0], out id);
            firstname = cols[1];
            lastname = cols[2];
            DateTime.TryParse(cols[3], out datetime_registered);
            email = cols[4];
            emailconfirmed = cols[5];
            emailconfirmationcode = cols[6];
            username = cols[7];
            problems = cols[8];
            problemdetails = cols[9];
        }
    }

    public string[] GetProblems()
    {
        if (problems != "")
            return problems.Split('*');
        else
            return null;
    }

    public void RemoveProblem(string problemtoremove)
    {
        string[] origProblems = GetProblems();
        string newProblems = "";
        foreach(string origProblem in origProblems)
        {
            if (origProblem != problemtoremove)
                newProblems += origProblem + "*";
        }

        if (newProblems != "")
            problems = newProblems.Remove(newProblems.Length - 1);
        else
            problems = newProblems;

    }

    public void AddProblem(string problemtoadd)
    {
        problems += problems == "" ? problemtoadd : "*" + problemtoadd;
    }

    public string[] GetProblemDetails()
    {
        return problemdetails.Split('*');
    }

    public void AddProblemDetail(string pdtoadd)
    {
        problemdetails += problemdetails == "" ? pdtoadd : "*" + pdtoadd;
    }
    public void RemoveProblemDetail(string pdtoremove)
    {
        string[] origPDs = GetProblemDetails();
        string newPD = "";
        foreach (string origPD in origPDs)
        {
            if (origPD != pdtoremove)
                newPD += origPD + "*";
        }

        if (newPD != "")
            problemdetails = newPD.Remove(newPD.Length - 1);
        else
            problemdetails = newPD;

    }


    public void Print()
    {
        Debug.Log("User: ID=" + id
            + ", FN=" + firstname
            + ", LN=" + lastname
            + ", UN=" + username
            + ", email=" + email
            + ", problems=" + problems
            + ", problemdetails=" + problemdetails
            );
    }
}

[Serializable]
public class Round
{
    public int id = -1;
    public string wanttoAchieve = "";
    public string problem = "";
    public int problemScale_Pre = -1; //0-10
    public int problemScale_Post = -1; //0-10
    public string problemdetail = "";
    public string bodypart = "";
    public string believe = "";
    public int believeScale_Pre = -1; //0-7
    public int believeScale_Post = -1; //0-7

    public Round() { }

    public void Print()
    {
        Debug.Log("Round: ID=" + id
    + ", wanttoAchieve=" + wanttoAchieve
    + ", problem=" + problem
    + ", problemScale_Pre=" + problemScale_Pre
    + ", problemdetail=" + problemdetail
    + ", bodypart=" + bodypart
    + ", believe=" + believe
    + ", believeScale_Pre=" + believeScale_Pre

    + ", problemScale_Post=" + problemScale_Post
    + ", believeScale_Post=" + believeScale_Post);
    }

}
