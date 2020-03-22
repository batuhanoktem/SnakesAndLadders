using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public List<Sprite> sides;

    public Action OnShuffle { get; set; }

    public Action OnThrow { get; set; }

    public Action OnRollFinished { get; set; }

    public Image Image { get; set; }

    public bool IsShuffle { get; set; }


    // Start is called before the first frame update
    void Awake()
    {
        Image = GetComponent<Image>();
    }

    public IEnumerator Roll()
    {
        OnShuffle?.Invoke();

        yield return new WaitUntil(() => IsShuffle);
        IsShuffle = false;
        
        OnThrow?.Invoke();
    }
}
