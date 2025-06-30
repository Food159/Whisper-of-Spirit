using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<string> videoFiles;
    private int videoIndex = 0;
    private void Start()
    {
        PlayCurrentVideo(videoIndex);
    }
    public void OnButtonNext()
    {
        videoIndex = (videoIndex + 1) % videoFiles.Count;
        PlayCurrentVideo(videoIndex);
    }
    public void OnButtonPrevious() 
    {
        videoIndex = (videoIndex - 1 + videoFiles.Count) % videoFiles.Count;
        PlayCurrentVideo(videoIndex);
    }
    void PlayCurrentVideo(int index)
    {
        if (videoPlayer != null && videoFiles.Count > 0)
        {
            string path = System.IO.Path.Combine(Application.streamingAssetsPath, videoFiles[index]);
            videoPlayer.url = path;
            videoPlayer.Play();
        }
    }
}
