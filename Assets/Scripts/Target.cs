using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    #region Private Variables
    private int targetNumber;
    #endregion

    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject spawnPosition;

    #region Editor Variables
    [SerializeField] private int max = 20;
    [SerializeField] private int min = 5;
    #endregion

    #region Unity Functions
    private void Awake()
    {
        targetNumber = Random.Range(min, max);
        UpdateUI();
        SpawnApples(targetNumber);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Apple") 
        {
            Apple a = other.gameObject.GetComponent<Apple>();
            Debug.Log("ouch");
            targetNumber -= a.GetPoints();
            UpdateUI();
            if (targetNumber <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion

    private void SpawnApples(int target)
    {

        List<List<int>> combinations = FindCombinations(target);
        List<int> combo = combinations[2];
        for (int i = 0; i < combo.Count; i++)
        {
            Apple newApple = Instantiate(applePrefab, spawnPosition.transform.position, Quaternion.identity).GetComponent<Apple>();
            newApple.SetPoints(combo[i]);
            
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

        // Result to store all combinations
        List<List<int>> result = new List<List<int>>();

        // Stack for iterative backtracking
        Stack<(List<int>, int, int)> stack = new Stack<(List<int>, int, int)>();

        // Start with an empty combination, remaining sum is target, and starting number is 1
        stack.Push((new List<int>(), target, 1));

        while (stack.Count > 0)
        {
            // Pop the current state from the stack
            var (currentCombination, remainingSum, start) = stack.Pop();

            // If remaining sum is 0, we've found a valid combination
            if (remainingSum == 0)
            {
                result.Add(new List<int>(currentCombination));
                continue;
            }

            // Iterate through possible numbers starting from 'start'
            for (int i = start; i <= target; i++)
            {
                if (i > remainingSum) break; // No need to continue if the number exceeds the remaining sum

                // Create a new combination with the current number added
                var newCombination = new List<int>(currentCombination) { i };

                // Push the new state to the stack
                stack.Push((newCombination, remainingSum - i, i)); // Allow reuse of the current number
            }
        }

        return result;
    }

    void PrintNestedList<T>(List<List<T>> nestedList)
    {
        foreach (var sublist in nestedList)
        {
            Debug.Log($"[{string.Join(", ", sublist)}]");
        }
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
