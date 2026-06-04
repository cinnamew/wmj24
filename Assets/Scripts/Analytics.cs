using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class Analytics : MonoBehaviour
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
