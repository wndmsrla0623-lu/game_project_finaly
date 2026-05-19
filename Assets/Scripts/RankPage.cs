using UnityEngine;
using System.Linq;
using TMPro;

public class RankPage : MonoBehaviour
{

    [SerializeField] Transform contentRoot;

    [SerializeField] GameObject rowprefab;


    StageResultList allData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList(1);
    }

   public void RefreshRankList(int stageIndex)
    {
        
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }


        var sortedData = allData.results.Where(r => r.stage == stageIndex).OrderByDescending(x => x.score).ToList();

        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowprefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }
}
