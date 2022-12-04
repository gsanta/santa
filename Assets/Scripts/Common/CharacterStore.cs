
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStore
{
    List<Character> _characters = new();

    private static CharacterStore _instance;

    public static CharacterStore GetInstance()
    {
        if (_instance == null)
        {
            _instance = new CharacterStore();
        }

        return _instance;
    }


    public void AddCharacter(Character character)
    {
        if (!_characters.Contains(character))
        {
            _characters.Add(character);
        }
    }

    public Character GetPlayer()
    {
        var character = _characters.Find((character) => character.IsPlayer() == true);
        return character;
    }

    public Character GetOneEnemy()
    {
        var character = _characters.Find((character) => character.IsPlayer() == false);
        return character;
    }

    public List<Character> GetEnemies()
    {
        var enemies = _characters.FindAll((character) => character.IsPlayer() == false).ToList();
        return enemies;
    }

    public Character GetClosestOpponent(Character character)
    {
        var player = GetPlayer();
        var enemies = GetOpponents(character);

        float minDist = float.MaxValue;
        Character enemy = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            float newDist = Vector2.Distance(player.GetPosition(), enemies[i].GetPosition());
            if (newDist < minDist)
            {
                minDist = newDist;
                enemy = enemies[i];
            }
        }

        return enemy;
    }

    public List<Character> GetOpponents(Character character)
    {
        var opponents = _characters.FindAll((currCharacter) => currCharacter.IsPlayer() != character.IsPlayer()).ToList();
        return opponents;
    }
}
