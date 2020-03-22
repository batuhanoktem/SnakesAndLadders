using System;

public interface IRollManager
{
    int activeNumber { get; set; }

    void Initialize();
    void Shuffle();
    void Throw();
    void Roll();
    void Enable();
    void Disable();
    Action OnRollFinished { get; set; }
}