using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChacarterSwap : MonoBehaviour
{

    //GameObject selectedCharacter;

    public void SwapCharacter(GameObject newChar)
    {
        GameObject _actualChar = GameObject.FindGameObjectWithTag("Player");
        if (newChar.name != _actualChar.name)
        {
            // Generar nuevo char
            GameObject _generatedChar = Instantiate(newChar, _actualChar.transform.position, _actualChar.transform.rotation);

            // Borrar el anterior
            Destroy(_actualChar);

            // Applicar layer y tag
            _generatedChar.layer = _actualChar.layer;
            _generatedChar.tag = _actualChar.tag;
            Character _characterScript = _generatedChar.GetComponent<Character>();

            // Swap level Manager prefab?
            TopDownEngineEvent.Trigger(TopDownEngineEventTypes.CharacterSwitch, _characterScript);

            // Trigger Camera events
            MMCameraEvent.Trigger(MMCameraEventTypes.SetTargetCharacter, _characterScript);
            MMCameraEvent.Trigger(MMCameraEventTypes.StartFollowing, _characterScript);
        }        
    }

}
