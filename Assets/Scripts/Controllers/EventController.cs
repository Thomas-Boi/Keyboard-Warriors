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

    //Enemy UI
    public List<HealthBar> healthbars;
    public List<Text> nameTexts;

    public SkillDialogue skillDialogue;

    public ActionMenu actionMenu; // uses the skill buttons now

    public GameObject winUIPrefab;
    public GameObject loseUIPrefab;
    public GameObject HUD;

    public Text waveTxt;

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
    private int wavesInWeek;

    public AudioController audioController;

    // Start is called before the first frame update
    public void Start()
    {
        weekData = JsonUtility.FromJson<WeekDatabase>(weekJson.text);
        skillManager = GetComponent<SkillManager>();
        ItemManager = GetComponent<ItemManager>();
        weekNum = ProgressTracker.GetTracker().WeekNum;
        wavesInWeek = weekData.weeks.Find(x => x.weekNum == weekNum).waves.Count;

        foreach (Character player in players)
        {
            if (weekNum > 1)
            {
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
                spawners[i].enemy.nameText = nameTexts[barNum];
                nameTexts[barNum].text = spawners[i].enemy.characterName;
                barNum++;
            }

        }



        for (int i = 0; i < players.Count; i++)
        {
            players[i].DisplayTurnMarker(false);
            players[i].HighlightPlayerName(false);
        }

        nextTurn();
        SetWaveText();
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
                players[turnNum - 1].HighlightPlayerName(false);
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
                players[turnNum].HighlightPlayerName(true);

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
            // Scale stress damage from 10%-25% depending on stress
            int damage = (int) Math.Floor(((Double) user.maxHealth * ((10 + (((Double) user.stress - 70) / 2)) / 100)));
            user.SetCharacterHealth(user.health - damage);
            checkLife();
            audioController.PlayPlayerStress(user.characterName);
            user.GetComponent<Animator>().Play("stress", 0, 0);
            //DisplayDamage(user, damage);
            DisplayHealthChange(user, damage, Color.red);
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

    // when character is to take damage, make text color red
    // or when they are being healed, make text color green
    public void DisplayHealthChange(Character target, float amount, Color color)
    {
        Vector3 charPosition = target.transform.position;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(charPosition);

        GameObject textObj = new GameObject("DamageText", typeof(RectTransform));
        textObj.transform.SetParent(HUD.transform);

        Text damageText = textObj.AddComponent<Text>();
        damageText.text = amount.ToString();
        damageText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        damageText.color = color;
        damageText.fontSize = 40;
        damageText.horizontalOverflow = HorizontalWrapMode.Overflow;
        damageText.verticalOverflow = VerticalWrapMode.Overflow;

        textObj.transform.position = screenPosition;

        StartCoroutine(FadeOutText(textObj));
    }

    private IEnumerator FadeOutText(GameObject textObject)
    {
        Text text = textObject.GetComponent<Text>();
        Color originalColour = text.color;

        float fadeOutTime = 1.5f;
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
            EventController.SetLayerRecursively(character.gameObject, 0);
        }
        foreach (Character character in players)
        {
            character.isTargetable = false;
            EventController.SetLayerRecursively(character.gameObject, 0);
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

    public static void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private void SetWaveText()
    {
        waveTxt.text = $"Wave {waveNum + 1} / {wavesInWeek}";
    }
}
