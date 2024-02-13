using System;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPlayer.Scripts
{
    public class VideosPlaylistUIHandler
    {
        public Action<int> OnSelectedItemEvent;
        public PlaylistItemView SelectedItem => _selectedItem;
        public int CollectionCount => _itemViews.Count;
        
        private const string PrefabPath = "PlaylistItem";
        
        private readonly Transform _content;
        private List<PlaylistItemView> _itemViews = new List<PlaylistItemView>();
        private PlaylistItemView _selectedItem;
        private PlaylistItemView _itemPrefab;

        public VideosPlaylistUIHandler(Transform content)
        {
            _content = content;
            _itemPrefab = Resources.Load<PlaylistItemView>(PrefabPath);
        }

        public void BuildItemsList(IEnumerable<PlaylistItemModel> itemsList)
        {
            foreach (var item in itemsList)
            {
                PlaylistItemView newPlaylistItem = GameObject.Instantiate(_itemPrefab);
                newPlaylistItem.Preview.sprite = item.Preview;
                newPlaylistItem.Title.text = item.Name;
                newPlaylistItem.transform.SetParent(_content, false);
                _itemViews.Add(newPlaylistItem);

                newPlaylistItem.Button.onClick.AddListener(() =>
                {
                    OnSelectedItemEvent?.Invoke(_itemViews.FindIndex(i => i == newPlaylistItem));
                });
            }
        }
        
        public void SelectItem(int idx)
        {
            if (_selectedItem != null)
            {
                _selectedItem.Deselect();
            }
            
            _selectedItem = _itemViews[idx];
            _selectedItem.Select();
        }
    }
}