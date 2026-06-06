using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine.SceneManagement;

// this is actually the analytics script
public class Beginning : Singleton<Beginning>
{
    async void Awake()
	{
		
        try
		{
			await UnityServices.InitializeAsync();
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}

        AnalyticsService.Instance.StartDataCollection();
	}
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        SceneLoadedEvent myEvent = new SceneLoadedEvent
        {
            SceneName = scene.name
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
    }
}

public class SceneLoadedEvent : Unity.Services.Analytics.Event
{
	public SceneLoadedEvent() : base("sceneLoaded")
	{
	}

	public string SceneName { set { SetParameter("sceneName", value); } }
}