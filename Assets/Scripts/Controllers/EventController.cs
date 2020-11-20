using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EventController : MonoBehaviour
{
    public TextAsset weekJson;

    public List<Spawner> spawners;
    public List<Character> players;
    public List<HealthBar> healthbars;

    public SkillDialogue skillDialogue;

    public ActionMenu actionMenu; // uses the skill buttons now

    public GameObject winUIPrefab;
    public GameObject loseUIPrefab;
    public GameObject HUD;

    // true = player turn
    // false = not player turn
    public bool playerTurn;
    //turn number within each team.
    // 0 = player1, 1 = player2
    public int turnNum = -1;
    public Action selectedAction;
    public int waveNum;
    public int weekNum;

    public static SkillManager skillManager { get; private set; }
    public static ItemManager ItemManager { get; private set; }

    public Text descriptionBox;
    public string tooltip = "";

    public WeekDatabase weekData;

    // Start is called before the first frame update
    public void Start()
    {
        weekData = JsonUtility.FromJson<WeekDatabase>(weekJson.text);
        skillManager = GetComponent<SkillManager>();
        ItemManager = GetComponent<ItemManager>();
        weekNum = ProgressTracker.GetTracker().WeekNum;

        foreach(Character player in players) {
            if (weekNum > 1) {
                CharStats stats = ProgressTracker.GetTracker().charStats.FirstOrDefault(x => player.id == x.id);
                player.exp = stats.exp;
                player.LevelUp(stats.level);
            }
        }


        StartWave();
        clearDescription();
        
    }


    public void StartWave()
    {
        turnNum = -1;
        /* switch (wave)
        {
            case 1:
                spawners[0].spawn("boxSlime");
                spawners[1].spawn("boxSlimeSmall");
                break;

            case 2:
                spawners[0].spawn("boxSlime");
                spawners[1].spawn("boxSlimeSmall");
                spawners[2].spawn("boxSlime");
                //spawners[3].spawn("boxSlimeSmall");
                //spawners[4].spawn("boxSlimeSmall");
                break;

            default:
                UnityEngine.Debug.Log("Invalid Wave");
                return;
        } */
        List<string> enemies = weekData.weeks.Find(x => x.weekNum == weekNum).waves[waveNum].enemies;
        int barNum = 0;
        for (int i = 0; i < spawners.Count; i++)
        {
            healthbars[i].transform.parent.gameObject.SetActive(false);
            if (i < enemies.Count && !String.IsNullOrEmpty(enemies[i]))
            {
                spawners[i].spawn(enemies[i]);
                healthbars[barNum].transform.parent.gameObject.SetActive(true);
                spawners[i].enemy.healthBar = healthbars[barNum];
                barNum++;
            }

        }



        for (int i = 0; i < players.Count; i++)
        {
            players[i].DisplayTurnMarker(false);
        }

        nextTurn();
    }


    public void nextTurn()
    {

        turnNum++;

        if (playerTurn)
        {
            actionMenu.DisplaySkillButtons(turnNum);
            // disable marker of previous player
            if ((turnNum - 1) >= 0)
            {
                players[turnNum - 1].DisplayTurnMarker(false);
            }
            if (turnNum < players.Count && players[turnNum].health <= 0)
            {
                nextTurn();

            }
            // enable marker of current player
            else if (turnNum >= 0 && turnNum < players.Count)
            {
                CheckStressChange(players[turnNum]);
                players[turnNum].DisplayTurnMarker(true);

                if (players[turnNum].health <= 0)
                {
                    nextTurn();
                }

            }

            if (turnNum >= players.Count)
            {
                turnNum = 0;
                playerTurn = false;
            }

        }
        if (!playerTurn)
        {
            actionMenu.EnableDisplay(false);

            if (turnNum >= spawners.Count)
            {
                turnNum = -1;
                playerTurn = true;
                nextTurn();
            }
            else if (spawners[turnNum].isOccupied)
            {
                skillManager.aiSelectSkill(spawners[turnNum].enemy);
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

    public void CheckStressChange(Character user)
    {
        int currentStress = user.stress;

        // when stress is full decrease character health
        if (!user.isEnemy && user.stress >= 70)
        {
            int damage = (int)user.maxHealth / 10;
            user.SetCharacterHealth(user.health - damage);
            checkLife();
            user.GetComponent<Animator>().Play("stress", 0, 0);
            DisplayDamage(user, damage);
        }
        if (!user.isEnemy)
        {
            user.SetCharacterStress(user.stress - 10);
        }

    }

    public void DisplaySkillDialogue(Character user, string skillName, float duration)
    {
        if (!user.isEnemy)
        {
            StartCoroutine(skillDialogue.DisplaySkill(skillName, duration));
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
        damageText.color = Color.red;
        damageText.fontSize = 35;
        damageText.horizontalOverflow = HorizontalWrapMode.Overflow;
        damageText.verticalOverflow = VerticalWrapMode.Overflow;

        textObj.transform.position = screenPosition;

        StartCoroutine(FadeOutText(textObj));

    }

    public void DisplayHeal(Character target, float damage)
    {
        Vector3 charPosition = target.transform.position;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(charPosition);

        GameObject textObj = new GameObject("DamageText", typeof(RectTransform));
        textObj.transform.SetParent(HUD.transform);

        Text damageText = textObj.AddComponent<Text>();
        damageText.text = damage.ToString();
        damageText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        damageText.color = Color.green;
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

    public void DisplayPlayerLose()
    {
        Instantiate(loseUIPrefab, HUD.transform);
    }

    public List<Character> getEnemies()
    {
        return spawners.FindAll(x => x.isOccupied).Select(x => x.enemy).ToList();
    }

    // return a list of players that are still alive.
    public List<Character> GetAlivePlayers()
    {
        return players.FindAll(x => x.health > 0);
    }

    // return a list of players that are dead.
    public List<Character> GetDeadPlayers()
    {
        return players.FindAll(x => x.health <= 0);
    }

    public void clearDescription()
    {
        tooltip = "";
        descriptionBox.text = "";
    }

    public void revertDescription()
    {
        descriptionBox.text = tooltip;
    }

    public void resetTargetting()
    {
        foreach (Character character in getEnemies())
        {
            character.isTargetable = false;
        }
        foreach (Character character in players)
        {
            character.isTargetable = false;
        }
    }

    public void ClearSpawners()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            if (spawners[i].enemy != null)
            {
                Destroy(spawners[i].enemy.gameObject);
                spawners[i].enemy = null;
            }
        }
    }

}
