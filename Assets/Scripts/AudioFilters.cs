using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using SimpleFileBrowser;

public class AudioFilters : MonoBehaviour
{
	public AudioMixerGroup lowPass, highPass, echo, distortion, flange, pitchShifter;
	public Text title, playButtonText;
	public GameObject loadingScreen;
	public Text loadingText;
	public GameObject mainScreen;

	public Sprite pressed;
	public Sprite notPressed;

	public Image[] buttons;

	public static AudioSource audioSource;

	void Awake ()
	{
		audioSource = GetComponent<AudioSource> ();

		// Set filters (optional)
		// It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
		// if all the dialogs will be using the same filters
		FileBrowser.SetFilters (true, new FileBrowser.Filter ("Music", ".ogg"));

		// Set default filter that is selected when the dialog is shown (optional)
		// Returns true if the default filter is set successfully
		// In this case, set Images filter as the default filter
		FileBrowser.SetDefaultFilter (".ogg");

		// Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
		// Note that when you use this function, .lnk and .tmp extensions will no longer be
		// excluded unless you explicitly add them as parameters to the function
		FileBrowser.SetExcludedExtensions (".lnk", ".tmp", ".zip", ".rar", ".exe");

		// Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
		// It is sufficient to add a quick link just once
		// Name: Users
		// Path: C:\Users
		// Icon: default (folder icon)
		FileBrowser.AddQuickLink ("Users", "C:\\Users", null);

	}

	public void OnLoadClick ()
	{
		StartCoroutine (ShowLoadDialogCoroutine ());
	}

	string filename = "";

	IEnumerator ShowLoadDialogCoroutine ()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog (false, null, "Select a song", "Load");
		if (FileBrowser.Success) {
				
			ShowLoading ("Loading: " + FileBrowser.Result + "..");

			// Dialog is closed
			// Print whether a file is chosen (FileBrowser.Success)
			// and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
			WWW myClip = new WWW ("file:///" + FileBrowser.Result);
			ShowLoading ("Loading: " + FileBrowser.Result + "..");
			string[] temp = FileBrowser.Result.Split ('\\');
			title.text = temp [temp.Length - 1].Replace (".ogg", "");
			filename = temp [temp.Length - 1].Replace (".ogg", "");
			AudioClip ac = myClip.GetAudioClip (false, false);

			while (ac.loadState != AudioDataLoadState.Loaded) {
				yield return null;
			}
			HideLoading ();
			audioSource.clip = ac;
			playButtonText.text = "Play";
		}
	}

	public void Play ()
	{
		if (audioSource.isPlaying) {
			audioSource.Stop ();
			playButtonText.text = "Play";
			audioSource.outputAudioMixerGroup = null;
		} else {
			title.text = string.IsNullOrEmpty (filename) ? "Audio Filters" : filename;
			audioSource.Play ();
			playButtonText.text = "Stop";
		}
	}

	public void EXIT ()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		return;
		#endif

		Application.Quit ();
	}

	void ShowLoading (string loadingTextt)
	{
		loadingScreen.SetActive (true);
		loadingText.text = loadingTextt;
	}

	void HideLoading ()
	{
		loadingScreen.SetActive (false);
		loadingText.text = "Loading...";
	}

	public void HighPassOnClick ()
	{
		foreach (Image img in buttons) {
			img.sprite = notPressed;
		}
		if (audioSource.outputAudioMixerGroup == highPass) {
			audioSource.outputAudioMixerGroup = null;
			buttons [0].sprite = notPressed;
		} else {
			audioSource.outputAudioMixerGroup = highPass;
			buttons [0].sprite = pressed;
		}
	}

	public void LowPassOnClick ()
	{
		foreach (Image img in buttons) {
			img.sprite = notPressed;
		}
		if (audioSource.outputAudioMixerGroup == lowPass) {
			audioSource.outputAudioMixerGroup = null;
			buttons [1].sprite = notPressed;
		} else {
			audioSource.outputAudioMixerGroup = lowPass;
			buttons [1].sprite = pressed;
		}
	}

	public void EchoOnClick ()
	{
		foreach (Image img in buttons) {
			img.sprite = notPressed;
		}
		if (audioSource.outputAudioMixerGroup == echo) {
			audioSource.outputAudioMixerGroup = null;
			buttons [4].sprite = notPressed;
		} else {
			audioSource.outputAudioMixerGroup = echo;
			buttons [4].sprite = pressed;
		}	
	}

	public void DistortionOnClick ()
	{
		foreach (Image img in buttons) {
			img.sprite = notPressed;
		}
		if (audioSource.outputAudioMixerGroup == distortion) {
			audioSource.outputAudioMixerGroup = null;
			buttons [2].sprite = notPressed;
		} else {
			audioSource.outputAudioMixerGroup = distortion;
			buttons [2].sprite = pressed;
		}	
	}

	public void FlangeOnClick ()
	{
		foreach (Image img in buttons) {
			img.sprite = notPressed;
		}
		if (audioSource.outputAudioMixerGroup == flange) {
			audioSource.outputAudioMixerGroup = null;
			buttons [3].sprite = notPressed;
		} else {
			audioSource.outputAudioMixerGroup = flange;
			buttons [3].sprite = pressed;
		}
	}

	public void PitchShifterOnClick ()
	{
		foreach (Image img in buttons) {
			img.sprite = notPressed;
		}
		if (audioSource.outputAudioMixerGroup == pitchShifter) {
			audioSource.outputAudioMixerGroup = null;
			buttons [5].sprite = notPressed;
		} else {
			audioSource.outputAudioMixerGroup = pitchShifter;
			buttons [5].sprite = pressed;
		}
	}
}
