using UnityEngine;
using System.Collections;
using System;

// Contiene la declaración de la clase Shoot, encargada de la mecánica de disparo.
// Permite dos formas de disparo exclusivas:
//      - Proyectiles
//      - Raycast
[RequireComponent(typeof(Recoil))]
public class Shoot : MonoBehaviour
{

    #region Exposed fields

    /// <summary>
    /// Proyectil a disparar. Si no está asignado, la mecánica de disparo utilizará
    /// Raycast para calcular los puntos de impacto
    /// </summary>
    /// 
    public Rigidbody m_projectile = null;

    /// <summary>
    /// Velocidad inicial del proyectil que se dispara
    /// </summary>
    public float m_InitialVelocity = 50f;

    /// <summary>
    /// Punto desde el que se dispara el proyectil
    /// </summary>
    public Transform m_ShootPoint;

    /// <summary>
    /// Tiempo transcurrido entre disparos
    /// </summary>
    public float m_TimeBetweenShots = 0.25f;

    /// <summary>
    /// Booleano para indicar si el arma es automática
    /// </summary>
    public bool m_IsAutomatic = false;

    /// <summary>
    /// Particulas que saltan cuando un arma sin proyectil acierta en algo.
    /// </summary>
    public GameObject m_Sparkles;

    /// <summary>
    /// Define el alcance del arma que no utiliza proyectiles.
    /// </summary>
    public float m_ShootRange = 100;

    /// <summary>
    /// Fuerza que aplican los disparos que no usan proyectiles.
    /// </summary>
    public float m_ShootForce = 10;

    /// <summary>
    /// Sonido del arma.
    /// </summary>
    public AudioClip m_ShootAudio;

    #endregion

    #region Non exposed fields

    /// <summary>
    /// Tiempo transcurrido desde el último disparo
    /// </summary>
    private float m_TimeSinceLastShot = 0;

    /// <summary>
    /// Indica si estamos disparando (util en modo automático).
    /// </summary>
    private bool m_IsShooting = false;

    private AudioSource audioSource = null;

    #endregion

    #region Monobehaviour Calls

    protected Camera cam;
    protected Recoil recoil;

    private void Start()
    {
        cam = Camera.main;
        recoil = GetComponent<Recoil>();
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// En el método Update se consultará al Input si se ha pulsado el botón de disparo
    /// </summary>
    void Update()
    {


        m_TimeSinceLastShot += Time.deltaTime * Time.timeScale;

        if (GetFireButton())
        {
            if (CanShoot())
            {
                recoil.SetRecoil();
                if (m_projectile != null)
                {
                    ShootProjectile();
                }
                else
                {
                    ShootRay();
                }
                m_TimeSinceLastShot = 0;

            }

            if (!m_IsShooting)
            {
                m_IsShooting = true;
                audioSource?.Play();
            }
        }
        else if (m_IsShooting)
        {
            m_IsShooting = false;
            audioSource?.Stop();
        }
    }

    private void ManageRecoil()
    {
    }

    // 
    /// <summary>
    /// En esta función comprobamos si el tiempo que ha pasado desde la última vez que disparamos
    /// es suficiente para que nos dejen volver a disparar 
    /// </summary>
    /// <returns>true si puede disparar y falso si no puede.</returns>
    private bool CanShoot()
    {
        return m_TimeSinceLastShot > m_TimeBetweenShots;
    }

    /// <summary>
    /// Devuelve si se ha pulsado el botón de disparo
    /// </summary>
    /// <returns>true si puede disparar y falso si no puede.</returns>
    private bool GetFireButton()
    {
        if (m_IsAutomatic)
        {
            return Input.GetButton("Fire1");
        }

        return Input.GetButtonDown("Fire1");
    }

    /// <summary>
    /// Disparamos un proyectil.
    /// </summary>
    private void ShootProjectile()
    {
        var projectile = Instantiate(m_projectile, m_ShootPoint.position, m_ShootPoint.rotation);
        projectile.velocity = projectile.transform.forward * m_InitialVelocity;
        var collider = projectile.GetComponent<Collider>();
        var myCollider = transform.root.GetComponent<Collider>();
        Physics.IgnoreCollision(collider, myCollider);
        Debug.Log("¡Pollo!");
    }

    /// <summary>
    /// Disparamos usando un rayo.
    /// </summary>
    private void ShootRay()
    {
        var ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, m_ShootRange))
        {
            hit.collider.attachedRigidbody?.AddForce(-hit.normal * m_ShootForce, ForceMode.Impulse);
            var particles = Instantiate(m_Sparkles, hit.point, Quaternion.LookRotation(-hit.normal)).GetComponent<Transform>();
            particles.Rotate(particles.right, 90);
        }
    }

    protected void OnDrawGizmos()
    {
        if (m_ShootPoint != null)
        {
            Debug.DrawRay(m_ShootPoint.position, m_ShootPoint.forward * 2f,
           Color.red);
        }
    }

    #endregion
}
