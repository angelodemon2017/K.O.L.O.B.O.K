using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character model", menuName = "Dialog/Character model", order = 1)]
public class CharactersConfig : ScriptableObject
{
    [SerializeField] private List<CharacterConfig> _characterConfigs;

    private Dictionary<ECharacters, CharacterConfig> _cachCharacters = new();

    public CharacterConfig GetCharacterConfig(ECharacters character)
    {
        if (!_cachCharacters.ContainsKey(character))
        {
            _cachCharacters.Clear();
            _characterConfigs.ForEach(c => _cachCharacters.Add(c.character, c));
        }

        return _cachCharacters[character];
    }
}