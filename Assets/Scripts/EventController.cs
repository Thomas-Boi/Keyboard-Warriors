using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    public List<Spawner> spawners;
    public List<Character> players;
    public List<HealthBar> healthbars;
    public List<SkillButton> skillButtons;
    public GameObject battleMenu;
    public GameObject winUIPrefab;
    public GameObject loseUIPrefab;
    public GameObject HUD;

    // true = player turn
    // false = not player turn
    public bool playerTurn;
    //turn number within each team.
    // 0 = player1, 1 = player2
    public int turnNum = -1;
    public string selectedSkill = "";
    public int waveNum;
    

    // Start is called before the first frame update
    void Start()
    {
        startWave(waveNum);
    }



    public void startWave(int wave)
    {
        turnNum = -1;
        switch (wave)
        {
            case 1:
                spawners[0].spawn("boxSlime");
                spawners[1].spawn("boxSlimeSmall");
                spawners[2].spawn("boxSlimeSmall");
                spawners[4].spawn("boxSlime");

                for (int i = 0; i < spawners.Count; i++)
                {
                    if (spawners[i].isOccupied)
                    {
                        spawners[i].enemy.healthBar = healthbars[i];
                    } 
                }

                for (int i = 0; i < players.Count; i++)
                {
                    players[i].DisplayTurnMarker(false);
                }

                nextTurn();
                break;
                

            default:
                UnityEngine.Debug.Log("Invalid Wave");
                break;
        }
    }


    public void displaySkills()
    {
        //these lists are temporary solutions. Will refactor and move this somewhere else later
        List<string> playerOne = new List<string> { "basicAttack", "basicAttack", "slimeyAttack" };
        List<string> playerTwo = new List<string> { "basicAttack", "basicAttack", "slimeyAttack", "slimeyAttack" };

        if (turnNum == 0)
        {
            for (int i = 0; i < playerOne.Count; i++)
            {
                skillButtons[i].spawnButton(playerOne[i], "p1Attack " + i);
            }
        }
        else
        {
            for (int i = 0; i < playerTwo.Count; i++)
            {
                skillButtons[i].spawnButton(playerTwo[i], "p2Attack " + i);
            }
        }
        
    }

    public void hideSkills()
    {
        foreach(SkillButton button in skillButtons)
        {
            button.gameObject.SetActive(false);
        }
    }



    public void nextTurn()
    {

        hideSkills();
        turnNum++;
        
        if (playerTurn)
        {

            // disable marker of previous player
            if ((turnNum - 1) >= 0)
            {
                players[turnNum - 1].DisplayTurnMarker(false);
            }
            // enable marker of current player
            if (turnNum >= 0 && turnNum < players.Count)
            {
                players[turnNum].DisplayTurnMarker(true);
            }

            displaySkills();
            if (turnNum >= players.Count)
            {
                turnNum = 0;
                playerTurn = false;
            }

        } 
        if (!playerTurn)
        {
            if (turnNum >= spawners.Count)
            {
                turnNum = -1;
                playerTurn = true;
                nextTurn();
            }
            else if (spawners[turnNum].isOccupied)
            {
                spawners[turnNum].enemy.selectSkill();
            }
            else
            {
                nextTurn();
            }

        }

        
        
    }

    public bool checkLife()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            if (spawners[i].isOccupied && spawners[i].enemy.health <= 0)
            {
                spawners[i].kill();
            }
        }
        if (players.Sum(p => p.health) <= 0)
        {
            DisplayPlayerLose();
            return false;
        }
        if (spawners.Count(s => s.isOccupied) <= 0)
        {
            DisplayPlayerWin();
            return false;
        }
        return true;
    }

    public void DisplayUsedSkill(Character target, string skill)
    {

    }

    public void CheckStressChange(Character character)
    {
        float currentStress = character.stress;
        float stressAmount = 30; // static amount for now

        // when stress is full decrease character health
        if (!character.isEnemy && character.stress == character.maxStress)
        {
            float currentHealth = character.health;
            float damage = 10;
            character.SetCharacterHealth(currentHealth - damage);
            DisplayDamage(character, damage);

        // when stress isn't full, increase it
        } else if (!character.isEnemy && character.stress < character.maxStress)
        {
            character.SetCharacterStress(currentStress + stressAmount);
        }
        
    }
    
    public void DisplayDamage(Character target, float damage)
    {
        Vector3 charPosition = target.transform.position;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(charPosition);

        GameObject textObj = new GameObject("DamageText", typeof(RectTransform));
        textObj.transform.SetParent(HUD.transform);

        Text damageText = textObj.AddComponent<Text>();
        damageText.text = "-" + damage.ToString();
        damageText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        damageText.color = Color.black;
        damageText.fontSize = 35;
        damageText.horizontalOverflow = HorizontalWrapMode.Overflow;
        damageText.verticalOverflow = VerticalWrapMode.Overflow;

        textObj.transform.position = screenPosition;

        StartCoroutine(FadeOutText(textObj));

    }

    private IEnumerator FadeOutText(GameObject textObject)
    {
        Text text = textObject.GetComponent<Text>();
        Color originalColour = text.color;

        float fadeOutTime = 1.0f;
        Vector3 move = new Vector3(0, 0.5f, 0);

        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColour, Color.clear, Mathf.Min(1, t / fadeOutTime));
            textObject.transform.position += move;
            yield return null;
        }

        Destroy(textObject);
    }


    private void DisplayPlayerWin()
    {
        Instantiate(winUIPrefab, HUD.transform);
    }

    private void DisplayPlayerLose()
    {
        Instantiate(loseUIPrefab, HUD.transform);
    }

}
