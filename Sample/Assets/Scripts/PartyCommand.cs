using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class PartyCommand : MonoBehaviour
{
    [SerializeField] private List<GameObject> commandBoxes;

    [SerializeField] private List<Button> heroCommand;
    [SerializeField] private List<Button> wizardCommand;
    [SerializeField] private List<Button> fencerCommand;
    [SerializeField] private List<Button> fighterCommand;

    [SerializeField] private List<Transform> partyTra;
    [SerializeField] private List<Transform> mobTra;
    [SerializeField] private List<GameObject> partyObj;
    [SerializeField] private List<GameObject> mobObj;

    [SerializeField] private List<Slider> partyHPbar;
    [SerializeField] private List<Slider> mobHPbar;

    [SerializeField] private GameObject clear;
    [SerializeField] private GameObject over;
    [SerializeField] private Button clearButton;
    [SerializeField] private Button overButton;

    [SerializeField] private GameObject mobField;

    [SerializeField] private GameObject waveObj;
    [SerializeField] private TextMeshProUGUI text;

    private List<int> selects;
    private int commandCount;

    private List<int> partyHP;
    private List<int> partyAttack;
    private List<int> partyMattack;
    private int partyDeathCount;

    private List<int> mobHP;
    private List<int> mobAttack;
    private int mobDeathCount;

    private int wave;

    [SerializeField] private Sprite boss;
    // Start is called before the first frame update
    void Start()
    {
        WaveClear();
        foreach(var bar in partyHPbar.Select((value, index) => new { value, index }))
        {
            bar.value.maxValue = partyHP[bar.index];
            bar.value.value = partyHP[bar.index];
        }

        foreach (var bar in mobHPbar.Select((value, index) => new { value, index }))
        {
            bar.value.maxValue = mobHP[bar.index];
            bar.value.value = mobHP[bar.index];
        }

        selects = new List<int>();

        foreach(var command in heroCommand.Select((value, index) => new { value, index }))
        {
            command.value.onClick.AsObservable()
                .Subscribe(_ => CommandSelect(heroCommand,command.index));
        }

        foreach (var command in wizardCommand.Select((value, index) => new { value, index }))
        {
            command.value.onClick.AsObservable()
                .Subscribe(_ => CommandSelect(wizardCommand,command.index));
        }

        foreach (var command in fencerCommand.Select((value, index) => new { value, index }))
        {
            command.value.onClick.AsObservable()
                .Subscribe(_ => CommandSelect(fencerCommand,command.index));
        }

        foreach (var command in fighterCommand.Select((value, index) => new { value, index }))
        {
            command.value.onClick.AsObservable()
                .Subscribe(_ => CommandSelect(fighterCommand,command.index));
        }

        clearButton.onClick.AsObservable()
            .Subscribe(_ => SceneManager.LoadScene("SampleScene"));

        overButton.onClick.AsObservable()
            .Subscribe(_ => SceneManager.LoadScene("SampleScene"));
    }

    private void CommandBoxSet(bool next = true)
    {
        if (next)
        {
            if (commandCount > 0)
            {
                commandBoxes[commandCount - 1].SetActive(false);
            }
            else
            {
                selects.Clear();
            }
            if (commandCount == commandBoxes.Count)
            {
                BattleMain();
                commandCount = 0;
                return;
            }
            commandBoxes[commandCount].SetActive(true);
            commandCount++;
        }
        else
        {
            commandCount--;
            commandBoxes[commandCount].SetActive(false);
            commandBoxes[commandCount - 1].SetActive(true);
        }
    }

    private void CommandSelect(List<Button> commands, int command)
    {
        if(commandCount != 1 && command == commands.Count - 1)
        {
            selects.RemoveAt(selects.Count - 1);
            CommandBoxSet(false);
        }
        else
        {
            selects.Add(command);
            CommandBoxSet(true);
        }
    }

    private void BattleMain()
    {
        int delayCount = 0;
        foreach (var chara in partyTra.Select((value, index) => new { value, index }))
        {
            if(partyHP[chara.index] <= 0) continue;
            delayCount++;
            DOVirtual.DelayedCall(delayCount + 1, () =>
            {
                foreach(var i in mobHP.Select((value, index) => new { value, index }))
                {
                    if (i.value <= 0) continue;
                    switch (chara.index)
                    {
                        case 0:
                            switch (selects[chara.index])
                            {
                                case 0:
                                    chara.value.DOPunchPosition(new Vector3(-10f, 0), 0.2f);
                                    mobTra[i.index].DOPunchPosition(new Vector3(-10f, 0), 0.2f);
                                    mobHP[i.index] -= partyAttack[chara.index];
                                    break;
                            }
                            break;
                        case 1:
                            switch (selects[chara.index])
                            {
                                case 0:
                                    chara.value.DOPunchPosition(new Vector3(-10f, 0), 0.2f);
                                    mobTra[i.index].DOPunchPosition(new Vector3(-10f, 0), 0.2f);
                                    mobHP[i.index] -= partyMattack[chara.index];
                                    break;
                                case 1:
                                    chara.value.DOPunchPosition(new Vector3(0, 10), 0.2f);
                                    foreach (var t in partyObj.Select((value, index) => new { value, index }))
                                    {
                                        partyHP[t.index] += (int)(partyMattack[chara.index] * 0.5f);
                                        if (partyHPbar[t.index].maxValue < partyHP[t.index])
                                        {
                                            partyHP[t.index] = (int)partyHPbar[t.index].maxValue;
                                        }
                                        partyHPbar[t.index].value = partyHP[t.index];
                                    }
                                    break;
                            }
                            break;
                        case 2:
                            switch (selects[chara.index])
                            {
                                case 0:
                                    chara.value.DOPunchPosition(new Vector3(-10f, 0), 0.2f);
                                    mobTra[i.index].DOPunchPosition(new Vector3(-10f, 0), 0.2f);
                                    mobHP[i.index] -= partyAttack[chara.index];
                                    break;
                            }
                            break;
                        case 3:
                            switch (selects[chara.index])
                            {
                                case 0:
                                    chara.value.DOPunchPosition(new Vector3(-10f, 0), 0.2f);
                                    mobTra[i.index].DOPunchPosition(new Vector3(-10f, 0), 0.2f);
                                    mobHP[i.index] -= partyAttack[chara.index];
                                    break;
                            }
                            break;
                    }
                    if (mobHP[i.index] <= 0)
                    {
                        mobHP[i.index] = 0;
                        mobObj[i.index].SetActive(false);
                        mobDeathCount++;
                    }
                    mobHPbar[i.index].value = mobHP[i.index];
                    if (mobDeathCount == mobObj.Count || (wave == 3 && mobDeathCount == 1))
                    {
                        DOVirtual.DelayedCall(6 - delayCount, () => WaveClear());
                        break;
                    }
                    if (chara.index == partyTra.Count - 1) MobAttack();
                    break;
                }
            });
        }
    }

    private void MobAttack()
    {
        int delayCount = 0;
        foreach (var mob in mobHP.Select((value, index) => new { value, index }))
        {
            if (mob.value <= 0)
            {
                if (mob.index == mobTra.Count - 1)
                {
                    DOVirtual.DelayedCall(delayCount + 1, () => CommandBoxSet());
                }
                continue;
            }
            delayCount++;
            DOVirtual.DelayedCall(delayCount + 1, () =>
            {
                mobTra[mob.index].DOPunchPosition(new Vector3(10f, 0), 0.2f);
                int rand = Random.Range(0, 4);
                while (partyHP[rand] <= 0)
                {
                    rand -= 1;
                    if (rand == -1) rand = 3;
                }
                partyTra[rand].DOPunchPosition(new Vector3(10f, 0), 0.2f);
                partyHP[rand] -= mobAttack[mob.index];
                if (partyHP[rand] <= 0)
                {
                    partyHP[rand] = 0;
                    partyObj[rand].SetActive(false);
                    partyDeathCount++;
                }
                partyHPbar[rand].value = partyHP[rand];
                if (partyDeathCount == partyObj.Count - 1)
                    DOVirtual.DelayedCall(1, () =>
                    {
                        GameOver();
                    });
                if (mob.index == mobTra.Count - 1) CommandBoxSet();
            });
        }
    }

    private void StatusSet()
    {
        switch (wave)
        {
            case 1:
                partyHP = new List<int>() { 500, 100, 300, 300 };
                partyAttack = new List<int>() { 100, 30, 120, 120 };
                partyMattack = new List<int>() { 30, 80, 10, 10 };

                mobHP = new List<int>() { 150, 150, 150 };
                mobAttack = new List<int>() { 50, 50, 50 };
                break;

            case 2:
                mobHP = new List<int>() { 150, 150, 150 };
                mobAttack = new List<int>() { 50, 50, 50 };
                break;

            case 3:
                mobHP = new List<int>() { 0, 1000, 0 };
                mobAttack = new List<int>() { 0, 80, 0 };
                break;
        }

        foreach(var hp in mobHP.Select((value, index) => new { value, index }))
        {
            if(hp.value == 0)
            {
                mobObj[hp.index].SetActive(false);
            }
            mobHPbar[hp.index].maxValue = hp.value;
            mobHPbar[hp.index].value = hp.value;
        }
    }

    private void WaveClear()
    {
        if (wave == 3)
        {
            clear.SetActive(true);
            return;
        }
        wave++;
        mobDeathCount = 0;
        if(wave == 3)
        {
            text.text = "BOSS BATTLE";
            mobObj[1].GetComponent<Image>().sprite = boss;
        }
        else
        {
            text.text = "wave" + wave;
        }
        mobField.SetActive(false);
        foreach(var mob in mobObj)
        {
            mob.SetActive(true);
        }
        mobField.SetActive(true);
        DOVirtual.DelayedCall(3f, () =>
        {
            waveObj.SetActive(true);
            waveObj.GetComponent<Image>().DOFade(0.5f, 1).SetLoops(2, LoopType.Yoyo);
            text.DOFade(1, 1).SetLoops(2, LoopType.Yoyo);
        });
        DOVirtual.DelayedCall(6f, () =>
        {
            CommandBoxSet();
            waveObj.SetActive(false);
        });
        StatusSet();
    }

    private void GameOver()
    {
        over.SetActive(true);
    }
}