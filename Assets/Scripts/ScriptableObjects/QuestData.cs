using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
[CreateAssetMenu(fileName = "QuestDataSO", menuName = "NewQuest")]
public class QuestData : ScriptableObject
{
    [SerializeField] private bool _isMainQuest;
    [SerializeField] private int _mainQuestIndex;

    [SerializeField] private Sprite _characterSprite;
    [SerializeField] private string _characterName;
    [SerializeField] private string _job;
    [SerializeField] private string _type;
    [TextArea(3, 10)]
    [SerializeField] private string _requestText;
    [TextArea(3, 10)]
    [SerializeField] private string _finalText;
    [SerializeField] private string[] _rewardText;

    public bool IsMainQuest { get => _isMainQuest; }
    public int MainQuestIndex { get => _mainQuestIndex; }

    public Sprite CharacterSprite { get => _characterSprite; }
    public string CharacterName { get => _characterName; }
    public string Job { get => _job; }
    public string Type { get => _type; }
    public string RequestText { get => _requestText; }
    public string FinalText { get => _finalText; }
    public string[] RewardText { get => _rewardText; }

#if UNITY_EDITOR
    [ContextMenu("Rename to name")]
    private void Rename()
    {
        this.name = _characterName + " " + _job + " " + _type ;
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(this);
    }
#endif

}
