﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private NetworkManager networkManager;
    private NetworkMatch networkMatch;
    private GameObject mainScreen, optionsScreen, aboutScreen, lanScreen, matchmakingScreen, menuCanvas;
    private GameObject currentScreen;
    private InputField inputField;

	// Use this for initialization
	void Start () {
        networkManager = GameObject.Find("NetworkManager").GetComponent<Manager>();
        menuCanvas = GameObject.Find("MenuCanvas");
        mainScreen = GameObject.Find("MainScreen");
        optionsScreen = GameObject.Find("OptionsScreen");
        aboutScreen = GameObject.Find("AboutScreen");
        lanScreen = GameObject.Find("LanScreen");
        matchmakingScreen = GameObject.Find("MatchmakingScreen");
        inputField = GameObject.Find("inputName").GetComponent<InputField>();
        lanScreen.SetActive(false);
        matchmakingScreen.SetActive(false);
        optionsScreen.SetActive(false);
        aboutScreen.SetActive(false);
        currentScreen = mainScreen;
    }

    public void switchScreen(GameObject screenToShow)
    {
        float time = 0.3f;
        StartCoroutine(FadeScreen(false, time, currentScreen));
        StartCoroutine(FadeScreen(true, time, screenToShow));
        currentScreen = screenToShow;
    }

    public void lanHost()
    {
        SceneManager.LoadScene("Game");
        networkManager.StartHost();
    }

    public void lanServerOnly()
    {
        networkManager.StartServer();
    }

    public void lanClient()
    {
        SceneManager.LoadScene("Game");
        networkManager.StartClient();
    }

    public void stopHost()
    {
        networkManager.StopHost();
        menuCanvas.SetActive(true);
        switchScreen(lanScreen);
    }

    public void enableMatchmaker()
    {
        networkManager.StartMatchMaker();
        switchScreen(matchmakingScreen);
    }

    public void disableMatchmaker()
    {
        networkManager.StopMatchMaker();
        switchScreen(lanScreen);
    }

    public void createMatch()
    {
        CreateMatchRequest request = new CreateMatchRequest();
        request.name = inputField.text;
        request.password = "";
        request.advertise = true;
        request.size = 2;
        networkManager.matchMaker.CreateMatch(request, OnInternetMatchCreate);
    }

    //this method is called when your request for creating a match is returned
    private void OnInternetMatchCreate(CreateMatchResponse matchResponse)
    {
        if (matchResponse != null && matchResponse.success)
        {
            //Debug.Log("Create match succeeded");

            MatchInfo hostInfo = new MatchInfo(matchResponse);
            NetworkServer.Listen(hostInfo, 9000);

            SceneManager.LoadScene("Game");
            networkManager.StartHost(hostInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    //call this method to find a match through the matchmaker
    public void FindInternetMatch(string matchName)
    {
        networkManager.matchMaker.ListMatches(0, 20, matchName, OnInternetMatchList);
    }

    //this method is called when a list of matches is returned
    private void OnInternetMatchList(ListMatchResponse matchListResponse)
    {
        if (matchListResponse.success)
        {
            if (matchListResponse.matches.Count != 0)
            {
                //Debug.Log("A list of matches was returned");

                //join the last server (just in case there are two...)
                networkManager.matchMaker.JoinMatch(matchListResponse.matches[matchListResponse.matches.Count - 1].networkId, "", OnJoinInternetMatch);
            }
            else
            {
                Debug.Log("No matches in requested room!");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }

    //this method is called when your request to join a match is returned
    private void OnJoinInternetMatch(JoinMatchResponse matchJoin)
    {
        if (matchJoin.success)
        {
            //Debug.Log("Able to join a match");


            MatchInfo hostInfo = new MatchInfo(matchJoin);
            SceneManager.LoadScene("Game");
            networkManager.StartClient(hostInfo);
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }

    public void quitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public IEnumerator FadeScreen(bool isActive, float time, GameObject objectToFade)
    {
        float alphaValue = 0.0f;
        if (isActive) { alphaValue = 1.0f; }
        Graphic[] graphics = objectToFade.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            graphic.CrossFadeAlpha(alphaValue, time, false);
        }
        if (!isActive)
        {
            yield return new WaitForSeconds(time);
        }
        objectToFade.SetActive(isActive);
    }
}
