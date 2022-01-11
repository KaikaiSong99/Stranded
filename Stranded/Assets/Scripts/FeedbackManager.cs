using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using TMPro;


public class FeedbackManager : MonoBehaviour
{

    public TextMeshProUGUI title;
    public GameObject chatboxPrefab;
    public Transform container;

    public float delayBetweenChats = 2.0f;
    public float delayBetweenJobs = 5.0f;

    private List<GameObject> chatboxes = new List<GameObject>();

    public IEnumerator Show(Dictionary<Job, Dictionary<Character, String>> feedback, Dictionary<Job, Character> picked)
    {

        foreach (var fb in feedback)
        {
            ClearBoxes();
            var job = fb.Key;
            Character assigned;

            //TODO Maybe do something with the character that was assigned
            // if (picked.TryGetValue(job, out assigned))
            // {
            //     title.text = $"{assigned.firstName} returned from {job.jobTitle.ToLower()}";
            // }
            // else 
            // {
            //     title.text = $"No one went {job.jobTitle}...";
            // }

            title.text = $"{job.jobTitle}";

            foreach(var characterFeedback in fb.Value)
            {
                var chatboxObject = Instantiate(chatboxPrefab, container);
                chatboxes.Add(chatboxObject);

                var chatbox = chatboxObject.GetComponent<Chatbox>();
                chatbox.Show(characterFeedback.Key, characterFeedback.Value);
                yield return new WaitForSeconds(delayBetweenChats);
            }

            yield return new WaitForSeconds(delayBetweenJobs);
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
