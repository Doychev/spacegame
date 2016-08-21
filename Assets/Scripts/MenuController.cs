using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private NetworkManager networkManager;
    private NetworkMatch networkMatch;
    private GameObject lanScreen, matchmakingScreen, menuCanvas;
    private InputField inputField;

	// Use this for initialization
	void Start () {
        networkManager = GameObject.Find("NetworkManager").GetComponent<Manager>();
        menuCanvas = GameObject.Find("MenuCanvas");
        lanScreen = GameObject.Find("LanScreen");
        matchmakingScreen = GameObject.Find("MatchmakingScreen");
        inputField = GameObject.Find("inputName").GetComponent<InputField>();
        matchmakingScreen.SetActive(false);
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
        lanScreen.SetActive(true);
        matchmakingScreen.SetActive(false);
    }

    public void enableMatchmaker()
    {
        networkManager.StartMatchMaker();
        lanScreen.SetActive(false);
        matchmakingScreen.SetActive(true);
    }

    public void disableMatchmaker()
    {
        networkManager.StopMatchMaker();
        lanScreen.SetActive(true);
        matchmakingScreen.SetActive(false);
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
}
