using System.Collections;
using TMPro;
using UnityEngine;

public class Nikolero : MonoBehaviour
{
	public GameObject config;
	public TMP_Dropdown subjectDrop;
	public TMP_Dropdown verbDrop;
	public TMP_Dropdown objectDrop;
	public TMP_Dropdown reasonDrop;

	public TMP_Text resultText;

	public Transform commentSection;
	public TMP_Text commentPrefab;

	public string[] userNames;
	public string[] comments;

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
			string userName = userNames[Random.Range(0, userNames.Length)];
			string comment = comments[Random.Range(0, comments.Length)];
			TMP_Text commentInstance = Instantiate(commentPrefab, commentSection);
			commentInstance.text = $"<b><color=green>@{userName}:</color></b> <color=white>{comment}</color>";
			yield return new WaitForSeconds(Random.Range(0.5f,1f));
		}
	}

}
