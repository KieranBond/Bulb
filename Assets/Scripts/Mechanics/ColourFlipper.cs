using UnityEngine;

public class ColourFlipper : MonoBehaviour
{
    [SerializeField]
    private ObstacleSpawner m_obstacleSpawns;

    [SerializeField]
    private Material m_setMaterial;

    [SerializeField]
    private Color[] m_changeableColours;

    private int m_colorIndex = 0;



    // Use this for initialization
    void Start()
    {
        m_obstacleSpawns.UpdatePrimaryColor(m_setMaterial.color);

    }

    void Update()
    {

    }

    public void UpdateColor()
    {
        //TODO : Lerp color?

        //Change the color and update our index.

        m_setMaterial.color = m_changeableColours[m_colorIndex];
        m_obstacleSpawns.UpdatePrimaryColor(m_changeableColours[m_colorIndex]);

        IncrementIndex();
    }

    private void IncrementIndex()
    {
        m_colorIndex++;

        if (m_colorIndex >= m_changeableColours.Length)
            m_colorIndex = 0;
    }
}
