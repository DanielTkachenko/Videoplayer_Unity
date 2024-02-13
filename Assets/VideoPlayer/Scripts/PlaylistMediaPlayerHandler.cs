using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEditor;
using UnityEngine;

namespace VideoPlayer.Scripts
{
    public class PlaylistMediaPlayerHandler
    {
        public PlaylistMediaPlayer PlaylistMediaPlayer => _playlistMediaPlayer;

        private const float TransitionTime = 1.0f;
        
        private PlaylistMediaPlayer _playlistMediaPlayer;
        
        public PlaylistMediaPlayerHandler(PlaylistMediaPlayer pmp)
        {
            _playlistMediaPlayer = pmp;
        }

        public void Setup()
        {
            _playlistMediaPlayer.LoopMode = PlaylistMediaPlayer.PlaylistLoopMode.Loop;
            _playlistMediaPlayer.AutoCloseVideo = true;
            _playlistMediaPlayer.AutoProgress = false;
            _playlistMediaPlayer.DefaultTransition = PlaylistMediaPlayer.Transition.Black;
            _playlistMediaPlayer.DefaultTransitionDuration = TransitionTime;
        }

        public void BuildPlaylist(IEnumerable<PlaylistItemModel> itemsList)
        {
            foreach (var item in itemsList)
            {
                MediaPlaylist.MediaItem newItem = new MediaPlaylist.MediaItem();

                newItem.startMode = PlaylistMediaPlayer.StartMode.Manual;
                newItem.name = item.Name;
                newItem.mediaPath = new MediaPath(item.Path, item.PathType);
                _playlistMediaPlayer.Playlist.Items.Add(newItem);
            }
        }

        public void PlayNext()
        {
            if (!_playlistMediaPlayer.NextItem())
            {
                Debug.LogError("Failed to play next item!");
            }
        }

        public void PlayPrevious()
        {
            if (!_playlistMediaPlayer.PrevItem())
            {
                Debug.LogError("Failed to play previous item!");
            }
        }

        public void PlayItem(int idx)
        {
            if (_playlistMediaPlayer.CanJumpToItem(2))
            {
                _playlistMediaPlayer.JumpToItem(2);
            }
            else
            {
                Debug.LogError($"Failed to play item at index {idx}!");
            }
        }
    }
}