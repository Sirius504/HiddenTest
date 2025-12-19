using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameTextMesh;

    public void Refresh(ItemInfo itemInfo, bool showIcon, bool showNames)
    {
        _iconImage.gameObject.SetActive(showIcon);
        _nameTextMesh.gameObject.SetActive(showNames);
        _iconImage.sprite = itemInfo.Icon;
        _nameTextMesh.text = itemInfo.Name;
    }
}
