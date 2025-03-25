using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("血量設定")]
    [SerializeField] protected float hpMax = 3;
    protected float hp;

    [Header("動畫參數")]
    [SerializeField] protected Animator ani;
    [SerializeField] protected string parDead = "死亡";

    [Header("血量顯示")]
    [SerializeField] protected Text hpText;

    [Header("音效設定")]
    [SerializeField] protected AudioClip soundHurt;     // 受傷音效
    [SerializeField] protected AudioClip soundDead;     // 死亡音效
    protected AudioSource aud;

    protected SpriteRenderer spriteRenderer;
    protected bool isHurt = false;
    protected bool isDead = false;
    protected string enemyName;

    protected virtual void Start()
    {
        hp = hpMax;
        enemyName = gameObject.name;

        if (ani == null) ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHpText();
    }

    public virtual void Hurt(float damage)
    {
        if (isHurt || isDead) return;

        hp -= damage;
        UpdateHpText();
        StartCoroutine(HurtFlash());

        if (soundHurt != null) aud.PlayOneShot(soundHurt);

        if (hp <= 0) Dead();

        Debug.Log($"<color=#33f>{enemyName}</color> 受傷，剩餘血量：<color=#f93>{hp}</color>");
    }

    protected IEnumerator HurtFlash()
    {
        isHurt = true;
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.05f); 
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
        isHurt = false;
    }

    protected virtual void Dead()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"<color=#f33>{enemyName} 死亡</color>");

        if (ani != null && !string.IsNullOrEmpty(parDead))
            ani.SetTrigger(parDead);

        if (soundDead != null) aud.PlayOneShot(soundDead);

        Destroy(gameObject, 0.5f);
    }

    protected void UpdateHpText()
    {
        if (hpText != null)
            hpText.text = $"HP: {hp}";
    }

    private void OnMouseDown()
    {
        Hurt(1);
    }
}