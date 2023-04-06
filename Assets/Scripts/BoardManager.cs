using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoardManager : MonoBehaviour
{
    [SerializeField] GameObject[] _pagePrefab;
    [SerializeField] GameObject _formPrefab;
    [SerializeField] QuestData_Side _sideQuestData;
    [SerializeField] QuestData _questData;

    private BoxCollider2D _bc;

    private GameObject _currentPage;

    private void Awake()
    {
        _bc = GetComponent<BoxCollider2D>();
    }

    public void SpawnPageRandom()
    {

        Vector2 spawnPoint = new Vector2(Random.Range(_bc.bounds.min.x, _bc.bounds.max.x),Random.Range(_bc.bounds.min.y, _bc.bounds.max.y));

        GameObject page = Instantiate(_pagePrefab[Random.Range(0, _pagePrefab.Length)], this.transform);
        GameObject form = Instantiate(_formPrefab, this.transform);

        page.transform.position = spawnPoint;
        form.transform.position = spawnPoint;

        page.GetComponent<DragAndDrop>().SetFormRef(form);
        form.GetComponent<QuestForm>().SetPageRef(page);

        form.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _sideQuestData.CharacterName[Random.Range(0, _sideQuestData.CharacterName.Length)];
        form.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _sideQuestData.Job[Random.Range(0, _sideQuestData.Job.Length)];
        form.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _sideQuestData.Type[Random.Range(0, _sideQuestData.Type.Length)];
        form.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = _sideQuestData.RequestText[Random.Range(0, _sideQuestData.RequestText.Length)];
        form.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _sideQuestData.RewardText[Random.Range(0, _sideQuestData.RewardText.Length)];
        form.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = _sideQuestData.RewardText[Random.Range(0, _sideQuestData.RewardText.Length)];
        form.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = _sideQuestData.RewardText[Random.Range(0, _sideQuestData.RewardText.Length)];
        form.transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = _sideQuestData.RewardText[Random.Range(0, _sideQuestData.RewardText.Length)];
        form.transform.GetChild(5).GetComponent<Image>().sprite = _sideQuestData.CharacterSprite[Random.Range(0, _sideQuestData.CharacterSprite.Length)];

        form.SetActive(false);
    }

    private void SpawnPage()
    {
        GameObject page = Instantiate(_pagePrefab[Random.Range(0, _pagePrefab.Length)], this.transform);
        GameObject form = Instantiate(_formPrefab, this.transform);

        page.GetComponent<DragAndDrop>().SetFormRef(form);

        form.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _questData.CharacterName;
        form.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _questData.Job;
        form.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _questData.Type;
        form.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = _questData.RequestText;
        form.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = _questData.RewardText[0];
        form.transform.GetChild(4).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = _questData.RewardText[1];
        form.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = _questData.RewardText[2];
        form.transform.GetChild(4).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = _questData.RewardText[3];
        form.transform.GetChild(5).GetComponent<Image>().sprite = _questData.CharacterSprite;

        form.SetActive(false);
    }

    public void PageCheck(GameObject pageRef)
    {
        if (_currentPage != pageRef)
        {
            if(_currentPage != null)
            {
                _currentPage.GetComponent<DragAndDrop>().ExitButton();
            }
            _currentPage = pageRef;
        }
    }
}
