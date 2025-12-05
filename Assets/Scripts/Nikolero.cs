using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class Nikolero : MonoBehaviour
{
	public string videoFileName = "nikola.mp4";
	public GameObject config;
	public TMP_Dropdown subjectDrop;
	public TMP_Dropdown verbDrop;
	public TMP_Dropdown objectDrop;
	public TMP_Dropdown reasonDrop;

	public TMP_Text resultText;

	public Transform commentSection;
	public TMP_Text commentPrefab;

	public List<string> userNames;
	public List<string> comments;

	public VideoPlayer videoPlayer;
	public AudioSource audioSource;


	private void Start()
	{
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
		videoPlayer.Prepare();
		videoPlayer.frame = Random.Range(0, 187);
		videoPlayer.Pause();
	}

	public void GenerateSentence()
	{		

		config.SetActive(false);
		string subject = subjectDrop.options[subjectDrop.value].text;
		string verb = verbDrop.options[verbDrop.value].text;
		string obj = objectDrop.options[objectDrop.value].text;
		string reason = reasonDrop.options[reasonDrop.value].text;

		string sentence = $"{subject} {verb} {obj} {reason}.";
		resultText.text = sentence;

		resultText.transform.parent.gameObject.SetActive(true);

		audioSource.Play();

		StartCoroutine(GeneratingComments());
	}

	public void RandomSentence()
	{
		// Randomly select options for each dropdown
		subjectDrop.value = Random.Range(0, subjectDrop.options.Count);
		verbDrop.value = Random.Range(0, verbDrop.options.Count);
		objectDrop.value = Random.Range(0, objectDrop.options.Count);
		reasonDrop.value = Random.Range(0, reasonDrop.options.Count);
		GenerateSentence();
	}

	IEnumerator GeneratingComments()
	{
		yield return new WaitForSeconds(0.5f);

		for (int i = 0; i < 20; i++)
		{
			int randomIndex = Random.Range(0, userNames.Count);
			string userName = userNames[randomIndex];
			userNames.RemoveAt(randomIndex);
			int commentIndex = Random.Range(0, comments.Count);
			string comment = comments[commentIndex];
			comments.RemoveAt(commentIndex);
			TMP_Text commentInstance = Instantiate(commentPrefab, commentSection);
			commentInstance.text = $"<b><color=#E1306C>@{userName}:</color></b> <color=#D4E2F1>{comment}</color>";
			yield return new WaitForSeconds(Random.Range(0.5f,1f));
		}
	}

}
