using System;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TreeService _treeService;
    [SerializeField] private PopupController _popupController;

    public event Action OnPlayerHit;
    public event Action OnPlayerDied;
    public event Action<string, float> OnTreeAnimationRequested;

    private void Start() => _player.OnPlayerMoved += WentPlayer;
    private void OnDestroy() => _player.OnPlayerMoved -= WentPlayer;

    private void WentPlayer(Vector2 playerPosition)
    {
        if (!VerifyDeath(playerPosition))
        {
            OnPlayerHit?.Invoke();
            OnTreeAnimationRequested?.Invoke(_treeService.GetCurrentTree().Id, playerPosition.x);
            _treeService.EditQueue();
        }
        
        if (VerifyDeath(playerPosition))
        {
            OnPlayerDied?.Invoke();
            ShowEndGamePopup("You died");
        }
    }

    private bool VerifyDeath(Vector2 playerPosition)
    {
        var tree = _treeService.GetCurrentTree();

        bool isDeath = (playerPosition == Vector2.left && tree.Type == ETreeType.Left
            || playerPosition == Vector2.right && tree.Type == ETreeType.Right);
        
        return isDeath;
    }

    private void ShowEndGamePopup(string message)
    {
        _popupController.ShowPopup(new EndGamePopupSettings
        {
            InfoText = message,
        });
    }
}
