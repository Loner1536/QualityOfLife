using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MelonLoader;

namespace QualityOfLife.Menu.ModManager.Interface;

public static class ModManagerScreen
{
    public static GameObject Create(GameObject settingsScreen)
    {
        GameObject modManagerScreen = Object.Instantiate(settingsScreen);
        modManagerScreen.transform.SetParent(settingsScreen.transform.parent, false);
        modManagerScreen.name = "ModManager";
        modManagerScreen.transform.SetSiblingIndex(4);

        SetTitle(modManagerScreen);
        ClearAndSetContent(modManagerScreen);
        UpdateButtons(modManagerScreen);

        return modManagerScreen;
    }

    private static void SetTitle(GameObject screen)
    {
        var title = screen.transform.Find("Title")?.GetComponent<TextMeshProUGUI>();
        if (title != null)
        {
            title.text = "Mod Manager";
            title.fontSize = 20;
        }
    }

    private static void ClearAndSetContent(GameObject screen)
    {
        Transform content = screen.transform.Find("Content");
        if (content)
        {
            foreach (Transform child in content)
            {
                if (child.name == "Display")
                {
                    foreach (Transform grandChild in child)
                        Object.Destroy(grandChild.gameObject);

                    Object.Destroy(child.GetComponent<VerticalLayoutGroup>());

                    var title = screen.transform.Find("Title");
                    var WIPTitle = Object.Instantiate(title.gameObject, child, false);
                    WIPTitle.name = "WIPTitle";
                    if (WIPTitle.TryGetComponent(out TextMeshProUGUI text))
                    {
                        text.text = "Work In Progress";
                        text.fontSize = 20;
                        WIPTitle.GetComponent<RectTransform>().anchoredPosition = new Vector2(240, -100);
                    }
                }
                else Object.Destroy(child.gameObject);
            }
        }
    }

    private static void UpdateButtons(GameObject screen)
    {
        Transform buttons = screen.transform.Find("Buttons");
        if (buttons)
        {
            foreach (Transform child in buttons)
            {
                if (child.name == "Display")
                {
                    var nameText = child.Find("Name")?.GetComponent<TextMeshProUGUI>();
                    if (nameText != null) nameText.text = "Test";
                    continue;
                }
                Object.Destroy(child.gameObject);
            }
        }
    }
}
