using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class FeedbackManager : MonoBehaviour
{

    public GameObject chatboxPrefab;
    public Transform container;

    public float delay = 1.0f;

    private List<GameObject> chatboxes = new List<GameObject>();

    

    public IEnumerator Show(Dictionary<Character, List<string>> feedback)
    {
        ClearBoxes();

        foreach (var fb in feedback)
        {
            foreach(var characterFeedback in fb.Value)
            {
                var chatboxObject = Instantiate(chatboxPrefab, transform);
                chatboxObject.transform.parent = container;

                var chatbox = chatboxObject.GetComponent<Chatbox>();
                chatbox.Show(fb.Key, characterFeedback);
                yield return new WaitForSeconds(delay);

            }
           
        }   

    }


    private void ClearBoxes()
    {
        foreach (var box in chatboxes)
        {
            Destroy(box);
        }
    }
}
