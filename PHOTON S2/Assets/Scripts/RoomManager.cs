using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
	public static RoomManager Instance;
	private bool Lola = false;

	void Awake()
	{
		if (Instance)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		Instance = this;
	}

	public override void OnEnable()
	{
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		if (scene.buildIndex == 1) // We're in the game scene
		{
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity); 
			if (Lola == false)
			 {
			 	Quaternion suu = Quaternion.Euler(-90, 0, 0);
			 	Vector3 pos = new Vector3(5, 0, 5);
			 	PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "orange cat"), pos, suu);
			 	Lola = true;
			 }
			// test lola spawn
			// if (this.PhotonNetwork.NickName == "0001")
			// {
			// 	Quaternion suu = Quaternion.Euler(-90, 0, 0);
			// 	Vector3 pos = new Vector3(5, 0, 5);
			// 	PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "orange cat"), pos, suu);
			// 	Lola = true;
			// }
		}
	}
}
