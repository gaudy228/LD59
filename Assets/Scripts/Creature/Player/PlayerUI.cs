using TMPro;
using UnityEngine;

public class PlayerUI : CreatureUI
{
    [SerializeField] private TextMeshProUGUI _manaText;
    private Player _player;

    public override void Subscription()
    {
        base.Subscription();
        _player = _creature as Player;
        _player.OnManaChanged += ChangeManaUI;
    }

    public override void UnSubscription()
    {
        base.UnSubscription();
        _player.OnManaChanged -= ChangeManaUI;
    }

    private void ChangeManaUI(int mana)
    {
        _manaText.text = $"{mana}/{_player.MaxMana}";
    }
}
