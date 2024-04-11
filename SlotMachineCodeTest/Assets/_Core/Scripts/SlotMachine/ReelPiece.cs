using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ReelPiece : MonoBehaviour
{
    public SpriteRenderer sprite;

    public int currentIndex = 0;
    public int reelStripIndex = 0;

    private Reel reelParent;

    public ReelPieceType reelPieceType;

    public bool setAsStopPiece = false;

    public void ResetPiece()
    {
        setAsStopPiece = false;
        stopActivated = false;
    }
    
    private void SetPieceType(ReelPieceType _type)
    {
        if (_type == null) { return; }
        
        reelPieceType = _type;
        sprite.sprite = reelPieceType.sprite;
    }

    private Tween movementTween;

    public void Move()
    {
        if (currentIndex + 1 == reelParent.reelSlots.Count)
        {
            currentIndex = 0;
            transform.position = reelParent.reelSlots[currentIndex].transform.position;
            
            SetPieceType(reelParent.GetNextReelPiece());

            reelStripIndex = reelParent.ReelPieceIndex - 1;
            setAsStopPiece = reelStripIndex == reelParent.ReelStopIndex; 
        }
        
        if (setAsStopPiece && reelParent.CanStop() && currentIndex == 2)
        {
            reelParent.Stop();
            return;
        }
        
        movementTween = transform.DOMove(reelParent.reelSlots[currentIndex + 1].transform.position, 0.2f)
            .SetEase(Ease.Linear)
            .OnComplete(OnMoveComplete);
    }

    private void OnMoveComplete()
    {
        currentIndex++;
        if (stopActivated) { return; }

        Move();
    }

    public void Init(int _currentIndex, Reel _reelParent, int _reelStripIndex)
    {
        reelStripIndex = _reelStripIndex;
        reelParent = _reelParent;
        currentIndex = _currentIndex;
        
        SetPieceType( reelParent.reelPieceTypeRegistry.GetSymbolTypeByName(reelParent.ReelStrip[_reelStripIndex]));
        transform.position = reelParent.reelSlots[currentIndex].transform.position;
    }

    private Tween animTween;
    public void Animate()
    {
        float punch = 0.1f;
        animTween = sprite.transform.DOPunchScale(new Vector3(punch, punch, punch), 1.5f)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }

    public void StopAnimation()
    {
        if (animTween.IsActive())
        {
            animTween.Kill();
        }
    }
    
    private bool stopActivated;
    
    public void Stop()
    {
        stopActivated = true;
        movementTween.Kill(); 
    }
}
