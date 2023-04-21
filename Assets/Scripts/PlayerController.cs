using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private float mouseSensitivityX = 3f;

    [SerializeField]
    private float mouseSensitivityY = 3f;

    [Header("Joint Options")]
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 50f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();
        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        if (PauseMenu.isOn)
        {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }

            motor.Move(Vector3.zero);
            motor.Rotate(Vector3.zero);
            motor.RotateCamera(0f);

            return;
        }

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, 100f))
        {
            joint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
        }
        else
        {
            joint.targetPosition = new Vector3(0f, 0f, 0f);
        }

        // Calculer la vélocité (vitesse) du mouvement de notre joueur
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        Vector3 moveHorizontal = transform.right * xMov;
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 velocity = (moveHorizontal + moveVertical) * speed;

        motor.Move(velocity);

        // On calcule la rotation du joueur en un Vector3
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;

        motor.Rotate(rotation);

        // On calcule la rotation de la camera en un Vector3
        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRotationX = xRot * mouseSensitivityY;

        motor.RotateCamera(cameraRotationX);
    }
        
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { positionSpring = _jointSpring, maximumForce = jointMaxForce };
    }
}
