using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour, IRollManager
{
    [SerializeField]
    private Dice dice;

    [SerializeField]
    private UnityEvent onShuffle;

    [SerializeField]
    private UnityEvent onThrow;

    [SerializeField]
    private UnityEvent onRollFinished;

    public int activeNumber { get; set; }

    public System.Action OnRollFinished { get; set; }

    public void Initialize()
    {
        dice.OnShuffle = () =>
        {
            onShuffle?.Invoke();
            Shuffle();
        };

        dice.OnThrow = () =>
        {
            onThrow?.Invoke();
            Throw();

        };
    }

    public void Shuffle()
    {
        StartCoroutine(ShuffleDice());
    }

    private IEnumerator ShuffleDice()
    {
        for (int i = 0; i <= 100; i++)
        {
            activeNumber = Random.Range(0, dice.sides.Count) + 1;
            dice.Image.sprite = dice.sides[activeNumber - 1];
            yield return new WaitForSeconds(0.01f);
        }
        dice.IsShuffle = true;
    }

    public void Throw()
    {
        onRollFinished?.Invoke();
        OnRollFinished.Invoke();
    }

    public void Roll()
    {
        StartCoroutine(dice.Roll());
    }

    public void Enable()
    {
        dice.GetComponent<Button>().interactable = true;
    }

    public void Disable()
    {
        dice.GetComponent<Button>().interactable = false;
    }
}
