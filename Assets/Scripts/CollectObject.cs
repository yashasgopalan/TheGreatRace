using System.Collections;
using UnityEngine;

public class CollectObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private float collectionDelay = 2.0f;
    [SerializeField] private AudioClip collectedAudioClip;
    [SerializeField] private AudioSource audioSource;
    
    bool isGrabbing = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void OnCollect()
    {
        if (isGrabbing) return;

        StartCoroutine(CollectionAction());
    }

    private IEnumerator CollectionAction()
    {
        isGrabbing = true;
        yield return new WaitForSeconds(collectionDelay);

        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(collectedAudioClip);
        }

        //TODO: SWAP deactivate with some other change
        gameObject.SetActive(false);

        isGrabbing = false;
    }

    void OnDisable()
    {
        isGrabbing = false;
        StopAllCoroutines();
    }
}
