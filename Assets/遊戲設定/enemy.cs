using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 敵人邏輯
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("血量設定")]
    [SerializeField] protected float hpMax = 3;
    protected float hp;

    [Header("動畫參數")]
    [SerializeField] protected Animator ani;
    [SerializeField] protected string parDead = "死亡"; // 

    [Header("血量顯示")]
    [SerializeField] protected Text hpText;

    protected SpriteRenderer spriteRenderer;
    protected bool isHurt = false;
    protected bool isDead = false;
    
    protected virtual void Start()
    {
        hp = hpMax;

        if (ani == null) ani = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateHpText();
    }

    /// <summary>
    /// 接收傷害
    /// </summary>
    public virtual void Hurt(float damage)
    {

        if (isHurt || isDead) return;

        hp -= damage;
        UpdateHpText();
        StartCoroutine(HurtFlash());

        if (hp <= 0) Dead();

        Debug.Log($"<color=#f93>敵人受傷，剩餘血量：{hp}</color>");
        
    }

    /// <summary>
    /// 閃爍受傷特效
    /// </summary>
    protected IEnumerator HurtFlash()
    {
        isHurt = true;
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.01f);
        }
        isHurt = false;
    }

    /// <summary>
    /// 死亡處理
    /// </summary>
    protected virtual void Dead()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("<color=#f33>敵人死亡</color>");
        if (ani != null && !string.IsNullOrEmpty(parDead))
            ani.SetTrigger(parDead);

        Destroy(gameObject, 0.5f); // 延遲刪除，保留死亡動畫時間
    }

    protected void UpdateHpText()
    {
        if (hpText != null)
            hpText.text = $"HP: {hp}";
    }

    // 點擊測試
    private void OnMouseDown()
    {
        Hurt(1);
    }
    
}
