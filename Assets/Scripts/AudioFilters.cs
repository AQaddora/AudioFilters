using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioFilters : MonoBehaviour
{
	public AudioMixerGroup lowPass, highPass, echo, distortion;
	public Text title, playButtonText;

	private AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Play()
	{
		if(audioSource.isPlaying)
		{
			audioSource.Stop();
			playButtonText.text = "Play";
			audioSource.outputAudioMixerGroup = null;
		}
		else
		{
			title.text = audioSource.clip.name;
			audioSource.Play();
			playButtonText.text = "Stop";
		}
	}

	public void LowPassOnClick()
	{
		audioSource.outputAudioMixerGroup = lowPass;
	}

	public void HighPassOnClick()
	{
		audioSource.outputAudioMixerGroup = highPass;
	}

	public void EchoOnClick()
	{
		audioSource.outputAudioMixerGroup = echo;
	}

	public void DistortionOnClick()
	{
		audioSource.outputAudioMixerGroup = distortion;
	}

	public void BackToNormalOnClick()
	{
		audioSource.outputAudioMixerGroup = null;
	}
}
