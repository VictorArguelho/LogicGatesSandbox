using UnityEngine;

public class SelectionCategoryButton : MonoBehaviour
{
    [SerializeField] private ItemCategoryCode _category;

    public void Select() =>
        SelectionMenu.Instance.ShowCategory(_category);
}