using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private List<Item> _items = new List<Item>();
    [SerializeField] private int _capacity = 8;
    [SerializeField] private Transform _dropPoint;

    [Header("UI")]
    public Image[] slotImages;
    public TextMeshProUGUI[] slotCounts;

    void Start()
    {
        UpdateHotbar();
    }

    void Update()
    {
        for (int i = 0; i < _capacity; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    DropItem(i);
                }
                else
                {
                    UseItem(i);
                }
            }
        }
    }

    public bool AddItem(Item newItem)
    {
        foreach (Item existingItem in _items)
        {
            if (existingItem.ID == newItem.ID)
            {
                existingItem.stackSize++;
                Destroy(newItem.gameObject);
                UpdateHotbar();
                return true;
            }
        }

        if (_items.Count >= _capacity)
        {
            return false;
        }

        _items.Add(newItem);
        newItem.OnPickup();

        _items.Sort((a, b) => a.ID.CompareTo(b.ID));

        UpdateHotbar();
        return true;
    }

    public void UseItem(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            Item item = _items[index];
            item.Use();

            item.stackSize--;

            if (item.stackSize <= 0)
            {
                _items.RemoveAt(index);
            }

            UpdateHotbar();
        }
    }

    public void DropItem(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            Item item = _items[index];

            Vector3 dropPos = transform.position + (transform.forward * 1.5f);
            if (_dropPoint != null)
            {
                dropPos = _dropPoint.position;
            }

            if (item.stackSize > 1)
            {
                item.stackSize--;

                GameObject droppedClone = Instantiate(item.gameObject, dropPos, Quaternion.identity);
                droppedClone.SetActive(true);

                Item cloneScript = droppedClone.GetComponent<Item>();
                if (cloneScript != null)
                {
                    cloneScript.stackSize = 1;
                }
            }
            else
            {
                item.gameObject.SetActive(true);
                item.transform.position = dropPos;

                _items.RemoveAt(index);
            }

            UpdateHotbar();
        }
    }

    private void UpdateHotbar()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < _items.Count)
            {
                slotImages[i].gameObject.SetActive(true);
                slotImages[i].sprite = _items[i].Icon;

                var c = slotImages[i].color;
                c.a = 1f;
                slotImages[i].color = c;

                if (slotCounts != null && i < slotCounts.Length && slotCounts[i] != null)
                {
                    if (_items[i].stackSize > 1)
                    {
                        slotCounts[i].text = _items[i].stackSize.ToString();
                        slotCounts[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        slotCounts[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                slotImages[i].gameObject.SetActive(false);

                if (slotCounts != null && i < slotCounts.Length && slotCounts[i] != null)
                {
                    slotCounts[i].gameObject.SetActive(false);
                }
            }
        }
    }
}