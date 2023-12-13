using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class SerialInfo
{
    public Bounds bounds = new(Random.onUnitSphere, Random.onUnitSphere);
    public Color color = new(Random.value, Random.value, Random.value);
    public Color32 color32 = new();
    public Dictionary<string, float> dictStr = new() { { "hello", 1 }, { "world", 2 } };
    public List<float> listFloat = new() { 1, 2, 3, 4, 5, 6 };
    public Quaternion quaternion = new(Random.value, Random.value, Random.value, Random.value);
    public Ray ray = new(Random.onUnitSphere, Random.onUnitSphere);
    public Rect rect = new(Random.value, Random.value, Random.value, Random.value);
    public Vector2 vector2 = Random.onUnitSphere;
    public Vector3 vector3 = Random.onUnitSphere;
    public Vector4 vector4 = Random.onUnitSphere;
}

public class LitJSONTest : MonoBehaviour
{
    private Format m_Format = Format.Pretty;
    private JsonWriter m_PrettyWriter;
    private Vector2 m_ScrollPos;

    private string m_str1 = "";

    // Use this for initialization
    private void Start()
    {
        m_PrettyWriter = new JsonWriter();
        m_PrettyWriter.PrettyPrint = true;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUI.enabled = m_Format != Format.Pretty;
        if (GUILayout.Button("PrettyFormat"))
        {
            m_Format = Format.Pretty;
        }

        GUI.enabled = m_Format != Format.Plain;
        if (GUILayout.Button("Plain Format"))
        {
            m_Format = Format.Plain;
        }

        GUILayout.EndHorizontal();

        GUI.enabled = true;
        if (GUILayout.Button("Serialize", GUILayout.Height(100)))
        {
            _TestSerialInfo();
        }

        m_ScrollPos = GUILayout.BeginScrollView(m_ScrollPos, GUILayout.Width(Screen.width - 200));
        if (m_str1.Length > 0)
        {
            GUILayout.TextArea(m_str1);
        }

        GUILayout.EndScrollView();
    }

    private void _TestSerialInfo()
    {
        var v1 = new SerialInfo();
        v1.vector2 = new Vector2(0.5f, 0.5f);

        if (m_Format == Format.Pretty)
        {
            m_PrettyWriter.Reset();
            JsonMapper.ToJson(v1, m_PrettyWriter);
            m_str1 = m_PrettyWriter.ToString();
        }
        else
        {
            m_str1 = JsonMapper.ToJson(v1); //serialize	object to string		
        }

        JsonMapper.ToObject<SerialInfo>(m_str1); //de-serialize string back to object			
    }

    private enum Format
    {
        Pretty,
        Plain
    }
}