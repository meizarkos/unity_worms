using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject[] weapons;

    public int health = 100;
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FireWeapon();
        }
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
        /**float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        if(moveInput != 0) {
            transform.rotation = Quaternion.Euler(0, moveInput < 0 ? 180 : 0 , 0);
        }
        float jumpInput = Input.GetAxis("Vertical");
        if(jumpInput > 0) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }**/
    }
    
    public void GoLeft() {
        rb.linearVelocity = new Vector2( -speed, rb.linearVelocity.y);
        transform.rotation = Quaternion.Euler(0, 180 , 0);
    }

    public void GoRight() {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        transform.rotation = Quaternion.Euler(0, 0 , 0);
    }

    public void Jump() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public void FireWeapon() {
        Weapon weapon = weapons[usingWeaponIndex].GetComponent<Weapon>();
        if (weapon == null)
        {
            Debug.LogError("Weapon component not found on the current weapon.");
        }
        weapon?.Fire();
        trajectory.DeactivateWhileFiring();
    }

    public void WeaponAddAngle()
    {
        weapons[usingWeaponIndex].GetComponent<Weapon>()?.AddAngle();
    }

    public void WeaponDecreaseAngle()
    {
        weapons[usingWeaponIndex].GetComponent<Weapon>()?.DecreaseAngle();
    }

    public void WeaponAddPower()
    {
        weapons[usingWeaponIndex].GetComponent<Weapon>()?.AddPower();
    }

    public void WeaponDecreasePower()
    {
        weapons[usingWeaponIndex].GetComponent<Weapon>()?.DecreasePower();
    }

    public void SwitchWeapon()
    {
        trajectory.Show(false);
        weapons[usingWeaponIndex].SetActive(false);
        usingWeaponIndex = (usingWeaponIndex + 1) % weapons.Length;
        weapons[usingWeaponIndex].SetActive(true);
        trajectory.Show(true);
    }

    public void TakeDamage(int damage)
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.PlayerLoseHp(damage);
    }
}
