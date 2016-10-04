using System;
using System.Collections.Generic;
using GenericExtensions;
using GenericExtensions.Factories;
using GenericExtensions.Interfaces;
using UnityEngine;
using Zenject;
using _Scripts.Backend.Models;
using _Scripts.Services;

namespace _Scripts.Ui
{
    public class LeaderBoardManager : MonoBehaviour
    {
        public int RowHeight;
        public int HeaderOffset;

        public GameObject ScrollableContainer;
        public GameObject RowsContainer;

        [Space(5)]
        public GameObject RowPrefab;

        [Space(10)]
        public Color FirstColor;
        public Color LastColor;

        private RectTransform _scrollableTransform;
        private RectTransform _rowContainerTransform;
        private ILoader _loader;
        private PrefabFactory _prefabFactory;

        private bool _isLoaded;

        [Inject]
        public void Initialize(ILoader loader, PrefabFactory prefabFactory)
        {
            _loader = loader;
            _prefabFactory = prefabFactory;
            _scrollableTransform = ScrollableContainer.GetComponent<RectTransform>();
            _rowContainerTransform = RowsContainer.GetComponent<RectTransform>();
        }

        public void LoadLeaderBoards()
        {
            if (_isLoaded) return;

            ScrollableContainer.SetActive(false);
            _loader.Loading(true);

            PersistentService.Instance.LeaderBoard.GetData((results, success) =>
            {
                if (!success)
                {
                    return;
                }

                _isLoaded = true;
                _loader.Loading(false);
                ScrollableContainer.SetActive(true);
                DrawLeaderBoardsTable(results);
            });
        }

        private void DrawLeaderBoardsTable(IList<LeaderBoardUser> users)
        {
            int scrollableHeight = RowHeight*users.Count + HeaderOffset;

            _scrollableTransform.sizeDelta = new Vector2(0, scrollableHeight);
            _rowContainerTransform.sizeDelta = new Vector2(0, scrollableHeight - HeaderOffset);

            float countR = 1f/users.Count;

            foreach (var user in users)
            {
                GameObject newRow = _prefabFactory.Create(RowPrefab);
                newRow.transform.SetParent(RowsContainer.transform, false);

                int rank = user.Rank ?? users.Count;

                Color newColor = users.Count == 1 
                    ? FirstColor 
                    : Color.Lerp(FirstColor, LastColor, rank*countR);

                LeaderBoardRow rowComponent =  newRow.FindComponent<LeaderBoardRow>();
                rowComponent.SetData(user);
                rowComponent.SetTextColor(newColor);
            }
        }
    }
}
