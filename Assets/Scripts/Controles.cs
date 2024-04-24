using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controles : MonoBehaviour
{
    [SerializeField] private float speed;
    public float pv;
    private Rigidbody2D body;
    //pour les collectibles
    [SerializeField] private Text collectible_counter;
    private int collectible = 0;
    private BoxCollider2D col;
    private bool invincible = false;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        //FLIP
        //if (horizontal < 0)
        //{
        //    spriteRenderer.flipX = !spriteRenderer.flipX;
        //} => retiré car le personnage avait juste des crises d'epilepsie.

        //ANIMATION
        anim.SetBool("run", horizontal!=0 || vertical!=0);

    }




    private void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag("Collectible"))
        {
            collectible += 1;
            collectible_counter.text = "" + collectible;
            Destroy(other.gameObject);
        }
        //mort du joueur et perte de pv  
        if (other.CompareTag("Ennemi") && !invincible)
        {
            anim.SetTrigger("hit");
            pv -= 1;
            Debug.Log(pv);
            if (pv == 0)
            {
                restart();
            }
            else
            {
                invincible = true;
            }
        }
    }
    public void IFrames()
    {
        Debug.Log("fin");
        invincible = false;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
