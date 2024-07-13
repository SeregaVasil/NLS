using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private KeyCode inventoryKey = KeyCode.I;
    [SerializeField] private Button inventoryButton;

    private List<string> inventoryItems = new List<string>();

    private void Start()
    {
        ValidateComponents();
        InitializeInventory();
        AddTestItems();
    }

    private void ValidateComponents()
    {
        if (inventoryPanel == null) Debug.LogError("Inventory Panel is not set in the inspector");
        if (itemsParent == null) Debug.LogError("Items Parent is not set in the inspector");
        if (itemPrefab == null) Debug.LogError("Item Prefab is not set in the inspector");
        if (inventoryButton == null) Debug.LogWarning("Inventory Button is not set in the inspector");
    }

    private void InitializeInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryButton?.onClick.AddListener(ToggleInventory);
    }

    private void AddTestItems()
    {
        AddItem("Меч");
        AddItem("Зелье");
        AddItem("Ключ");
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI();
        }
    }

    public void AddItem(string itemName)
    {
        inventoryItems.Add(itemName);
        if (inventoryPanel.activeSelf)
        {
            UpdateInventoryUI();
        }
    }

    private void UpdateInventoryUI()
    {
        if (itemsParent == null || itemPrefab == null) return;

        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (string item in inventoryItems)
        {
            GameObject newItemGO = Instantiate(itemPrefab, itemsParent);
            TMP_Text itemText = newItemGO.GetComponentInChildren<TMP_Text>();
            if (itemText != null)
            {
                itemText.text = item;
            }
            else
            {
                Debug.LogError("TextMeshPro component not found in item prefab");
            }
        }
    }
}