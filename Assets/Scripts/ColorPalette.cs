using UnityEngine;

[CreateAssetMenu(fileName = "New Palette", menuName = "Palette")]
public class ColorPalette: ScriptableObject
{
    public string paletteName;
    public Color[] palette;
}