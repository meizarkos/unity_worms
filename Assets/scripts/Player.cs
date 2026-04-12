using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject[] weapons;
    private int usingWeaponIndex = 0;
    public float speed = 5f;
    public float jumpForce = 3f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>(); // aller chercher objet de unity
        weapons[usingWeaponIndex].SetActive(true);
        for(int i = 0; i < weapons.Length; i++) {
            if(i != usingWeaponIndex) {
                weapons[i].SetActive(false);
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapon();
        }
        FireWeapon();
    }

    // Update is called once per frame
    void FixedUpdate() {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        if(moveInput != 0) {
            transform.rotation = Quaternion.Euler(0, moveInput < 0 ? 180 : 0 , 0);
        }
        float jumpInput = Input.GetAxis("Vertical");
        if(jumpInput > 0) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FireWeapon() {
        if (Input.GetButton("Fire1"))
        {
            Weapon weapon = weapons[usingWeaponIndex].GetComponent<Weapon>();
            weapon?.Fire();   
        }
    }
    void SwitchWeapon()
    {
        weapons[usingWeaponIndex].SetActive(false);
        usingWeaponIndex = (usingWeaponIndex + 1) % weapons.Length;
        weapons[usingWeaponIndex].SetActive(true);
    }
}
