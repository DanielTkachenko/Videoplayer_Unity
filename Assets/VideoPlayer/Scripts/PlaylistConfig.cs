using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

namespace VideoPlayer.Scripts
{
    [CreateAssetMenu(fileName = "PlaylistConfig", menuName = "Configs/PlaylistConfig")]
    public class PlaylistConfig : ScriptableObject
    {
        public IReadOnlyList<PlaylistItemModel> Items => items;
        
        [SerializeField] private List<PlaylistItemModel> items;
    }

    [Serializable]
    public class PlaylistItemModel
    {
        public string Name;
        public Sprite Preview;
        public MediaPathType PathType;
        public string Path;
    }
}