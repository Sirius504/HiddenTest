using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameTextMesh;

    public void Refresh(ItemInfo itemInfo, bool isIconActive)
    {
        _iconImage.gameObject.SetActive(isIconActive);
        _iconImage.sprite = itemInfo.Icon;
        _nameTextMesh.text = itemInfo.Name;
    }
}
