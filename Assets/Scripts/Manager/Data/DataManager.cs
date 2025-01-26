using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    // --- �̱������� ���� --- //
    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if(!instance)
            {
                // ���ο� ������Ʈ ����
                container = new GameObject();

                // ������Ʈ �̸� ����
                container.name = "DataManager";

                // ���ο� ������Ʈ�� ������ �Ŵ��� �ְ�, �ν��Ͻ��� �Ҵ�
                instance = container.AddComponent(typeof(DataManager)) as DataManager;

                // �ش� ������Ʈ�� ������� �ʵ��� ����
                DontDestroyOnLoad(container);
            }
            return instance; 
        }
    }

    // -- ���� ������ �����̸� ���� ("���ϴ� �̸�(����).json") --- //
    string GameDataFileName = "GameData.json";

    // -- ����� Ŭ���� ���� -- //
    public GameData data = new GameData();

    // �ҷ�����
    public void LoadGameData()
    {
        // ���� ��� ����
        String filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
            String FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            SaveGameData();
        }
    }

    // �����ϱ�
    public void SaveGameData()
    {
        // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
        string ToJsonData = JsonUtility.ToJson(data, true);
        // ���� ��� ����
        String filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
        File.WriteAllText(filePath, ToJsonData);
    }

    // ������ �����ϸ� �ڵ�����
    void OnApplicationQuit()
    {
        SaveGameData();
    }
}
