using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    // --- 싱글톤으로 선언 --- //
    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if(!instance)
            {
                // 새로운 오브젝트 생성
                container = new GameObject();

                // 오브젝트 이름 설정
                container.name = "DataManager";

                // 새로운 오브젝트에 데이터 매니저 넣고, 인스턴스에 할당
                instance = container.AddComponent(typeof(DataManager)) as DataManager;

                // 해당 오브젝트가 사라지지 않도록 유지
                DontDestroyOnLoad(container);
            }
            return instance; 
        }
    }

    // -- 게임 데이터 파일이름 설정 ("원하는 이름(영문).json") --- //
    string GameDataFileName = "GameData.json";

    // -- 저장용 클래스 변수 -- //
    public GameData data = new GameData();

    // 불러오기
    public void LoadGameData()
    {
        // 파일 경로 설정
        String filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            String FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            SaveGameData();
        }
    }

    // 저장하기
    public void SaveGameData()
    {
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        // 파일 경로 설정
        String filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);
    }

    // 게임을 종료하면 자동저장
    void OnApplicationQuit()
    {
        SaveGameData();
    }
}
