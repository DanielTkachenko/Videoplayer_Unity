using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VideoPlayer.Scripts;

public class VideosPlaylist : MonoBehaviour
{
    private const string ConfigPath = "PlaylistConfig";
    [SerializeField] private PlaylistMediaPlayer playlistMediaPlayer;
    [SerializeField] private Transform content;

    [Header("UI Elements")] [Space(10)]
    [SerializeField] private Button forwardButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Text videoTitle;

    private PlaylistConfig config;
    private PlaylistMediaPlayerHandler _playlistMediaPlayerHandler;
    private VideosPlaylistUIHandler _videosPlaylistUIHandler;
    
    private int _selectedItemIndex;

    // Start is called before the first frame update
    void Start()
    {
        config = Resources.Load<PlaylistConfig>(ConfigPath);
        _playlistMediaPlayerHandler = new PlaylistMediaPlayerHandler(playlistMediaPlayer);
        _videosPlaylistUIHandler = new VideosPlaylistUIHandler(content, videoTitle);
        _selectedItemIndex = 0;
        
        forwardButton.onClick.AddListener(PlayNext);
        backButton.onClick.AddListener(PlayPrev);
        _videosPlaylistUIHandler.OnSelectedItemEvent += Select;
        
        _playlistMediaPlayerHandler.Setup();
        LoadItemsList();
        
        _videosPlaylistUIHandler.SelectItem(_selectedItemIndex);
    }

    private void LoadItemsList()
    {
        _playlistMediaPlayerHandler.BuildPlaylist(config.Items);
        _videosPlaylistUIHandler.BuildItemsList(config.Items);
    }

    private void Select(int idx)
    {
        if (idx == _selectedItemIndex)
            return;
        _selectedItemIndex = idx;
        _videosPlaylistUIHandler.SelectItem(_selectedItemIndex);
        _playlistMediaPlayerHandler.PlayItem(_selectedItemIndex);
    }

    private void PlayNext()
    {
        if (_selectedItemIndex < _videosPlaylistUIHandler.CollectionCount - 1)
        {
            _selectedItemIndex++;
            _videosPlaylistUIHandler.SelectItem(_selectedItemIndex);
            _playlistMediaPlayerHandler.PlayNext();
        }
    }

    private void PlayPrev()
    {
        if (_selectedItemIndex > 0)
        {
            _selectedItemIndex--;
            _videosPlaylistUIHandler.SelectItem(_selectedItemIndex);
            _playlistMediaPlayerHandler.PlayPrevious();
        }
    }
}
