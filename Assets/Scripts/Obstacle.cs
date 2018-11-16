using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private Color m_myDefinedColor;

    private Material m_instancedMaterial;

    private void Start()
    {
        //Creates an instance of our prime material and sets the color if possible.
        SetInstancedMaterial();
    }

    public void InformColour(Color a_myDefinedColor)
    {
        m_myDefinedColor = a_myDefinedColor;

        if (m_instancedMaterial != null)
        {
            m_instancedMaterial.color = m_myDefinedColor;
            //m_instancedMaterial.SetColor("Albedo", m_myDefinedColor);
        }
        else
        {
            SetInstancedMaterial();
        }
    }

    private void SetInstancedMaterial()
    {
        Debug.Log("Setting instanced material");
        m_instancedMaterial = new Material(GetComponent<SpriteRenderer>().material);
        GetComponent<SpriteRenderer>().material = m_instancedMaterial;

        if(m_myDefinedColor != null)
            m_instancedMaterial.color = m_myDefinedColor;
            //m_instancedMaterial.SetColor("Albedo", m_myDefinedColor);
    }
}
