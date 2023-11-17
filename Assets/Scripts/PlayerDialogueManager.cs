// using System.Collections;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class PlayerDialogueManager : MonoBehaviour
// {
//     public PlayerController playerControllerCs;
//     private bool isScared = false;
    
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         isScared = playerControllerCs.isScared;
//         if(isScared && dialogues.Length > 0)
//         {
//             Debug.Log("Before StartCoroutine");
//             StartCoroutine(DisplayLine(dialogues[0]));
//             Debug.Log("After StartCoroutine");
//         }
//     }

//     private IEnumerator DisplayLine(string line)
//     {

//         foreach (char letter in line.ToCharArray())
//         {
//             dialogueText.text += letter;
//             yield return new WaitForSeconds(typingSpeed);
//         }
//     }
// }
