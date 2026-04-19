using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject[] weapons;
    private int usingWeaponIndex = 0;
    public float speed = 5f;
    public float jumpForce = 3f;
    private Rigidbody2D rb;
    private TrajectoryDisplay trajectory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        trajectory = GetComponentInChildren<TrajectoryDisplay>();
    
        rb = GetComponent<Rigidbody2D>(); // aller chercher objet de unity
        for(int i=0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[usingWeaponIndex].SetActive(true);
        trajectory.GetComponent<TrajectoryDisplay>().Show(true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapon();
        }
        FireWeapon();
        Weapon currentWeapon = weapons[usingWeaponIndex].GetComponent<Weapon>();
        
        trajectory.UpdateDots(
            currentWeapon.firePoint.position,
            currentWeapon.firePoint.right,
            currentWeapon.startingAngle,
            currentWeapon.GetPower(),
            currentWeapon.timeStep,
            usingWeaponIndex == 0
        );
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
            trajectory.DeactivateWhileFiring();   
        }
    }
    void SwitchWeapon()
    {
        trajectory.Show(false);
        weapons[usingWeaponIndex].SetActive(false);
        usingWeaponIndex = (usingWeaponIndex + 1) % weapons.Length;
        weapons[usingWeaponIndex].SetActive(true);
        trajectory.Show(true);
    }
}
