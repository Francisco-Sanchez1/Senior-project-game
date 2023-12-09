using UnityEngine;
using UnityEngine.Video;


public class VideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component

    void Start()
    {
        videoPlayer.playOnAwake = false;
    }

    public void PlayVideo()
    {
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += PreparedVideo;
        }
    }

    private void PreparedVideo(VideoPlayer source)
    {
        videoPlayer.Play();
    }
}