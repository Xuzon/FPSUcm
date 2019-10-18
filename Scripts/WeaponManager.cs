using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Clase que gestiona el cambio de arma del jugador.
// En función de las teclas pulsadas por el usuario
// permite a éste alternar entre las armas disponibles
public class WeaponManager : MonoBehaviour
{


    /// <summary>
    /// Variable pública que contiene referencias a las armas del player
    /// El orden en el que se asignan corresponde al número de tecla que le corresponde
    /// </summary>
    public List<GameObject> m_Weapons;

    /// <summary>
    /// Variable privada que contiene el arma activa en cada momento
    /// </summary>
    private GameObject m_ActiveWeapon;

    /// <summary>
    /// Índice del arma por defecto en el manager
    /// </summary>
    public int m_DefaultWeaponIndex = 0;

    /// <summary>
    /// Ininicializaciones
    /// </summary>
    void Start()
    {

        if (m_Weapons.Count == 0)
        {
            Debug.LogWarning("Please assign at least a weapon to the weapon manager");
            return;
        }
        var toActive = m_Weapons[m_DefaultWeaponIndex];
        toActive.SetActive(true);
        m_ActiveWeapon = toActive;

    }

    // En el método Update estaremos leyendo de la entrada de usuario para ver qué tecla
    // se pulsa. En caso de ser alguna numérica, gestionaremos las armas, teniendo cuidado
    // de que sólo haya un arma activa en cada momento
    void Update()
    {
        for(int i = 0; i < 9; i++)
        {
            if(Input.GetKey(KeyCode.Alpha1 + i))
            {
                ManageWeapon(i);
                return;
            }
        }
    }

    // Dicho número indicará el índice del arma que se quiere activar/desacivar
    void ManageWeapon(int index)
    {
        var toActive = m_Weapons[index];
        if (toActive == m_ActiveWeapon)
        {
            return;
        }
        m_ActiveWeapon.SetActive(false);
        m_ActiveWeapon = toActive;
        m_ActiveWeapon.SetActive(true);
    }
}
