using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Reel : MonoBehaviour
{
    [Header("Elements")]
    public List<GameObject> reelSlots;
    public List<ReelPiece> reelPieces;
    
    [Header("Info")]
    public ReelPieceTypeRegistry reelPieceTypeRegistry;
    public Directions movementDirection;

    private List<string> reelStrip;
    public List<string> ReelStrip => reelStrip;

    public int fullSpinDone = 0;
    
    private int positionInSlotMachine;

    private int reelStopIndex;
    public int ReelStopIndex => reelStopIndex;

    private bool stopActivated = false;

    private SlotMachine slotMachine;

    public void Setup(List<string> _reelStrip, int _positionInSlotMachine, SlotMachine _slotMachine)
    {
        slotMachine = _slotMachine;
        positionInSlotMachine = _positionInSlotMachine;
        reelStrip = _reelStrip;
        
        Init();
    }

    public void ResetReel()
    {
        stopActivated = false;
        
        reelPieces[0].Init(0, this, GetResetedReelIndex(reelPieceIndex, 2));
        reelPieces[1].Init(1, this, GetResetedReelIndex(reelPieceIndex, 1)); 
        reelPieces[2].Init(2, this, reelPieceIndex); // Center
        reelPieces[3].Init(3, this, GetResetedReelIndex(reelPieceIndex, -1));
    }

    private int GetResetedReelIndex(int _index, int _v)
    {
        var result = _index + _v; 
        
        if (result >= reelStrip.Count)
        {
            return 0;
        }
        
        if(result < 0)
        {
            return reelStrip.Count -1;
        }

        return result;
    }

    public void Spin(int _reelStopIndex)
    {
        reelStopIndex = _reelStopIndex;

        if (slotMachine.FirstSpinDone)
        {
            ResetReel();
        }
        
        foreach (var reelPiece in reelPieces)
        {
            reelPiece.ResetPiece();
            reelPiece.Move();
        }
    }

    public void InitStop()
    {
        stopActivated = true;
    }

    public void Stop()
    {
        slotMachine.OnReelStopped();
        foreach (var reelPiece in reelPieces)
        {
            reelPiece.Stop();
        }
    }

    private int reelPieceIndex = 0;
    public int ReelPieceIndex => reelPieceIndex;

    public ReelPieceType GetNextReelPiece()
    {
        reelPieceIndex++;

        if (reelPieceIndex >= reelStrip.Count)
        {
            fullSpinDone++;
            reelPieceIndex = 0;
        }
        
        return reelPieceTypeRegistry.GetSymbolTypeByName(reelStrip[reelPieceIndex]);
    }

    private int IncreaseAndGetReelPieceIndex()
    {
        return reelPieceIndex++;
    }

    public bool CanStop()
    {
        return stopActivated && fullSpinDone > 0;
    }

    private void Init()
    {
        reelPieces[3].Init(3, this, IncreaseAndGetReelPieceIndex());
        reelPieces[2].Init(2, this, IncreaseAndGetReelPieceIndex());
        reelPieces[1].Init(1, this, IncreaseAndGetReelPieceIndex());
        reelPieces[0].Init(0, this, IncreaseAndGetReelPieceIndex());
    }
}
