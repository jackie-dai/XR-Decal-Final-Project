using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    #region Private Variables
    private int targetNumber;
    private List<GameObject> apples = new List<GameObject>();
    private GameManager gameManager;
    #endregion

    #region Serialized Variables
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private int player = 1;
    #endregion

    #region Editor Variables
    [SerializeField] private int max = 20;
    [SerializeField] private int min = 5;
    [SerializeField] private float offset = 1f;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        targetNumber = Random.Range(min, max);
        UpdateUI();
        SpawnApples(targetNumber);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("Game Manager not found");
        }
    }

    public void Reset()
    {
        DestroyAllApples();
        targetNumber = Random.Range(min, max);
        UpdateUI();
        SpawnApples(targetNumber);
    }

    public void AssignPlayer(int playerChoice)
    {
        player = playerChoice;
    }

    private void DestroyAllApples()
    {
        // Destroy all apples
        foreach (var apple in apples)
        {
            Destroy(apple);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Apple") 
        {
            Apple a = other.gameObject.GetComponent<Apple>();
            targetNumber -= a.GetPoints();
            Destroy(a.gameObject);
            UpdateUI();
            if (targetNumber == 0)
            {
                gameManager.RoundOver(player);
            } else if (targetNumber < 0) 
            { 
                this.Reset();
            }
        }
    }
    #endregion

    private void SpawnApples(int target)
    {

        List<List<int>> combinations = FindCombinations(target);
        List<List<int>> slice = combinations.GetRange(2, 4);
        List<int> sums = FlattenNestedList(slice);

        // beginning position = transform.position - (offset * (num_apples / 2))
        Vector3 startPos = spawnPosition.transform.position - new Vector3(offset * (sums.Count / 2), 0, 0);

        for (int i = 0; i < sums.Count; i++)
        {
            Apple newApple = Instantiate(applePrefab, startPos + new Vector3(offset * i, 0, 0), Quaternion.identity).GetComponent<Apple>();
            newApple.SetPoints(sums[i]);
            apples.Add(newApple.gameObject);
        }
    }

    private List<List<int>> FindCombinations(int target)
    {
        // 5 =
        // 1 + 1 + 1 + 1 + 1
        // 1 + 1 + 1 + 2
        // 1 + 2 + 2
        // 2 + 1 + 1 + 1
        // 2 + 2 + 1
        // 3 + 1 + 1
        // 3 + 2
        // 4 + 1

        List<List<int>> result = new List<List<int>>();

        Stack<(List<int>, int, int)> stack = new Stack<(List<int>, int, int)>();

        stack.Push((new List<int>(), target, 1));

        while (stack.Count > 0)
        {
            var (currentCombination, remainingSum, start) = stack.Pop();

            if (remainingSum == 0)
            {
                result.Add(new List<int>(currentCombination));
                continue;
            }

            for (int i = start; i <= target; i++)
            {
                if (i > remainingSum) break;

                var newCombination = new List<int>(currentCombination) { i };
                stack.Push((newCombination, remainingSum - i, i)); 
            }
        }

        return result;
    }

    private void PrintList(List<int> list)
    {
        Debug.Log("List contents: " + string.Join(", ", list));
    }

    private void PrintNestedList<T>(List<List<T>> nestedList)
    {
        foreach (var sublist in nestedList)
        {
            Debug.Log($"[{string.Join(", ", sublist)}]");
        }
    }

    private List<int> FlattenNestedList(List<List<int>> nestedLists)
    {
        List<int> flatList = new List<int>();

        foreach (var sublist in nestedLists)
        {
            flatList.AddRange(sublist); 
        }

        return flatList;
    }

    private void UpdateUI()
    {
        scoreUI.text = targetNumber.ToString();
    }

    #region Public Functions
    public int GetTarget()
    {
        return targetNumber;
    }
    #endregion

}
