using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab; // 弾の設計図を入れる枠
    public float bulletSpeed = 5f; // 弾の速さ

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // --- 移動処理 ---
        float moveX = 0;
        float moveY = 0;
        if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed) moveX = 1f;
        if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed) moveX = -1f;
        if (keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed) moveY = -1f;
        if (keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed) moveY = 1f;
        transform.Translate(new Vector3(moveX, moveY, 0) * speed * Time.deltaTime);

        // --- 弾を打つ処理（スペースキーを押した瞬間） ---
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 1. 弾を生成する
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        // 2. 弾を飛ばす（Rigidbody 2D を使って右方向に力を加える）
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * bulletSpeed; // 三角形の向いている「上」へ飛ばす
        
        // 3. 2秒後に自動で消えるようにする（溜まりすぎ防止）
        Destroy(bullet, 2f);
    }
}