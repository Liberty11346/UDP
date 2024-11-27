using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ExperienceEntry
{
    public int level;
    public int experienceRequired;
}

public class ExperienceTable : MonoBehaviour
{
    private List<ExperienceEntry> experienceTable;

    // 생성자에서 데이터 초기화
    public ExperienceTable()
    {
        experienceTable = new List<ExperienceEntry>();
        LoadExperienceTable();
    }

    // JSON 파일에서 경험치 테이블 로드
    private void LoadExperienceTable()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "ExpTable.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ExperienceTableWrapper tableWrapper = JsonUtility.FromJson<ExperienceTableWrapper>(json);
            experienceTable = tableWrapper.entries;
        }
        else
        {
            Debug.LogError("경험치 테이블이 존재하지 않음");
        }
    }

    // 레벨에 맞는 경험치를 가져오는 함수
    public int GetExpForLevel(int level)
    {
        foreach (ExperienceEntry entry in experienceTable)
        {
            if (entry.level == level)
            {
                return entry.experienceRequired;
            }
        }
        return 0; // 해당 레벨에 대한 경험치가 없으면 0 반환
    }

    [System.Serializable]
    private class ExperienceTableWrapper
    {
        public List<ExperienceEntry> entries;
    }
}
