using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSwaper : MonoBehaviour
{
    GameObject Player;

    public GameObject PlayerSkills;

    public GameObject PrevSkillPanel;
    public GameObject NextSkillPanel;

    public List<string> skillNames = new List<string>();

    int skillIndex = 0;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        SetFirstSkills(skillNames.Count);


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ++skillIndex;
            if (skillIndex == skillNames.Count - 1) //если на последнем навыке
            {
                gameObject.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex]);
                NextSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[0]);
                PrevSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex - 1]);
            }

            else if (skillIndex >= skillNames.Count) //переход с полсденего на первый
            {
                skillIndex = 0;
                gameObject.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex]);
                NextSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex + 1]);
                PrevSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillNames.Count - 1]);
            }

            else
            {
                gameObject.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex]);
                NextSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex + 1]);
                PrevSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex - 1]);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            --skillIndex;
            if (skillIndex == 0) //если на первом
            {
                gameObject.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex]);
                NextSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex + 1]);
                PrevSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillNames.Count - 1]);
            }

            else if (skillIndex <= -1) //переход с первого на последний
            {
                skillIndex = skillNames.Count - 1;
                gameObject.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex]);
                NextSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[0]);
                PrevSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex - 1]);
            }

            else
            {
                gameObject.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex]);
                NextSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex + 1]);
                PrevSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[skillIndex - 1]);
            }
        }
    }

    public string CurrentSkill()
    {
        return skillNames[skillIndex];
    }

    public void AddToList(string name)
    {
        skillNames.Add(name);
    }

    void SetFirstSkills(int count)
    {
        switch (count)
        {
            case 1:
                gameObject.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[0]);
                break;
            case 2:
                NextSkillPanel.SetActive(true);
                //NextSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[1]);
                break;
            case 3:
                PrevSkillPanel.SetActive(true);
                //NextSkillPanel.GetComponent<Image>().sprite = PlayerSkills.GetComponent<PlayerSkills>().GetSkillSprite(skillNames[2]);
                break;
            default:
                break;
        }
    }
}
