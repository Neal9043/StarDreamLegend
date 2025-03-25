using UnityEngine;

public class ClickManager : MonoBehaviour
{ 
    [Header("點擊造成的傷害值")]
    public float damage = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 左鍵點擊
        {
            ClickTarget();
        }
    }

    void ClickTarget()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
           
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Hurt(damage);
            }
        }
    }


}

