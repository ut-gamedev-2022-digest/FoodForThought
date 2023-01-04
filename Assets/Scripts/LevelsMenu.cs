using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    public GameObject[] levelButtons;

    private void OnRenderObject()
    {
        var levelsUnlocked = Game.GetLevelsUnlockedForCurrentUser();
        Debug.Log($"On render, levelsUnlocked = {levelsUnlocked}");
        for (var i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<Button>().interactable = i < levelsUnlocked;
        }
    }
}