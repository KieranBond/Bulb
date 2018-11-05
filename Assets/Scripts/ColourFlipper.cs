using UnityEngine;

public class ColourFlipper : MonoBehaviour
{
    [SerializeField]
    private Material[] m_changeableMaterials;


    private Color[] m_materialColors;

    private int m_colorIndex = 0;

    private Material m_setMaterial;
    // Use this for initialization
    void Start()
    {
        m_setMaterial = new Material(m_changeableMaterials[0]);
        m_materialColors = new Color[m_changeableMaterials.Length];

        int n = 0;
        foreach(Material current in m_changeableMaterials)
        {
            m_materialColors[n] = current.color;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
