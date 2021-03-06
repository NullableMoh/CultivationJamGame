using UnityEngine;

public class ToggleWithRope : MonoBehaviour
{
    [SerializeField] private bool hideOnMergeable;
    [SerializeField] private Canvas toHide;
    
    private RopePile currentPile;
    private RopePileRevealer ropePileRevealer;

    private void OnEnable()
    {
        ropePileRevealer = FindObjectOfType<RopePileRevealer>();
        ropePileRevealer.OnRopePileRevealed += FindRopePile;
    }

    private void FindRopePile()
    {
        currentPile = FindObjectOfType<RopePile>();
        currentPile.OnPileHit += OnPileMergeable;
        currentPile.OnPileNotHit += OnPileNotMergeable;
        currentPile.OnPileDestroyed += RemovePile;
        currentPile.OnPileDestroyed += OnPileNotMergeable;
    }

    private void OnDisable()
    {
        if(!currentPile) return;
        
        currentPile.OnPileHit -= OnPileMergeable;
        currentPile.OnPileNotHit -= OnPileNotMergeable;
        currentPile.OnPileDestroyed -= RemovePile;
        currentPile.OnPileDestroyed -= OnPileNotMergeable;
    }

    private void OnPileMergeable()
    {
        currentPile = FindObjectOfType<RopePile>();
        toHide.enabled = !hideOnMergeable;
    }

    private void OnPileNotMergeable()
    {
        currentPile = FindObjectOfType<RopePile>();
        toHide.enabled = hideOnMergeable;
    }

    private void RemovePile()
    {
        currentPile.OnPileHit -= OnPileMergeable;
        currentPile.OnPileNotHit -= OnPileNotMergeable;
        currentPile.OnPileDestroyed -= RemovePile;
        currentPile.OnPileDestroyed -= OnPileNotMergeable;
    }
}
