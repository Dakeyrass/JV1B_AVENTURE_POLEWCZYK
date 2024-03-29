using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controles : MonoBehaviour
{
    [SerializeField] private float speed;
    public float pv;
    private Rigidbody2D body;
    //pour les collectibles
    [SerializeField] private Text collectible_counter;
    private int collectible = 0;
    private BoxCollider2D col;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //DEPLACEMENTS
        Vector2 deplacements = new Vector2(horizontal, vertical);
        //On applique la v�locit� au rgdbd et on la multiplie par la vitesse souhait�e
        deplacements.Normalize();
        body.velocity = deplacements * speed;
    }

    private void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag("Ennemi")){
            pv -= 1;
            if (pv<=0){
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Collectible"))
        {
            collectible += 1;
            collectible_counter.text = "" + collectible;
            Destroy(other.gameObject);
        }
    }
}
