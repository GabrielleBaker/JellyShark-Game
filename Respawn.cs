using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject secondStart;
    public GameObject player;
    public GameObject death;
    public float flashDuration = 0.1f; // Duration of each flash
    public int numberOfFlashes = 2; // Number of times to flash
    private SpriteRenderer playerSpriteRenderer;
    private AudioSource audioSource;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        audioSource = death.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play ();
            StartCoroutine(FlashPlayer());
        }
    }
    private IEnumerator FlashPlayer()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            playerSpriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }
        RespawnPlayer();
    }
    // Update is called once per frame
    private void RespawnPlayer()
    {
        if (count == 0) {
            player.transform.position = startPoint.transform.position;
            count ++;
        }
        else
        {
            player.transform.position = secondStart.transform.position;
            count = 0;
        }
       // player.transform.position = startPoint.transform.position;
        player.transform.rotation = Quaternion.identity;
    }
}
