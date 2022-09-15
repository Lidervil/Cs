namespace ConsoleApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class kaiti{


//add to repo
    public Text userName;
    public Text email;
    public Text password;
    
    public bool userKnown; 
    public bool mailKnown; 

    public static void Init() {
        if (!Directory.Exists(USER_FOLDER));
        Directory.CreateDirectory(USER_FOLDER);
    }

    public static void SignIn() {

        string mailUser;
        string pwUser;
        string nameUser;
        string name = userName.text;
        string mail = email.text;
        string passw = password.text;

        nameUser = name + ".txt";

        if (File.Exists("USER_FOLDER/"+nameUser))
        {
            print("Mail ya registrado");
            userKnown = true;
        }

        DirectoryInfo dirInfo = new DirectoryInfo(USER_FOLDER);
        FileInfo[] registedUsers = dirInfo.GetFiles();
        foreach (FileInfo user in registedUsers) {
            SaveObject userLoaded = JsonUtility.FromJson<SaveObject>(user);
            if (userLoaded.email==mail) {
                mailKnown = true;
                break;
            }
        }

        if (userKnown) break; 
        if (mailKnown) break;


        SaveObject userObject = new SaveObject {
            email = mail,
            user = name,
            pass = passw
        };

        string json = JsonUtility.ToJson(userObject);

        /* Debug.Log(json);

        SaveObject userLoaded = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log(userLoaded.email); */

        File.WriteAllText(Application.dataPath+"USER_FOLDER/" + nameUser, json);
    }

    public static void Login() {

        string mail = email.text;
        string passw = password.text;

        string userLogin;
        string mailLogin;
        string pwLogin;

        DirectoryInfo dirInfo = new DirectoryInfo(USER_FOLDER);
        FileInfo[] registedUsers = dirInfo.GetFiles();
        foreach (FileInfo user in registedUsers) {
            SaveObject userLoaded = JsonUtility.FromJson<SaveObject>(user);
            if (userLoaded.email==mail) {
                if (userLoaded.pass==passw) {
                    userLogin = userLoaded.user;
                    mailLogin = userLoaded.email;
                    pwLogin = userLoaded.pass;
                    break;
                } else {
                    print("Password not found :(");
                    break;
                }
            } else {
                print("User not found :D");
                break;
            }
        }
    }
}