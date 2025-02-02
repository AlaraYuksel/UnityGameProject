using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform miniMapIndicator; // Mini haritada gösterilecek ikon
    public RectTransform miniMapPanel; // Mini harita panelinin RectTransform'ý

    private void Update()
    {
        if (miniMapIndicator == null) return;

        // Mini harita pozisyonunu hesaplama
        Vector3 worldPosition = transform.position; // Dünya pozisyonu
        Vector2 miniMapPosition = new Vector2(worldPosition.x, worldPosition.z); // 2D pozisyon

        // Mini haritayý doldurma
        miniMapIndicator.localPosition = new Vector3(miniMapPosition.x, miniMapPosition.y, 0);
    }
}

