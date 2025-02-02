using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform miniMapIndicator; // Mini haritada g�sterilecek ikon
    public RectTransform miniMapPanel; // Mini harita panelinin RectTransform'�

    private void Update()
    {
        if (miniMapIndicator == null) return;

        // Mini harita pozisyonunu hesaplama
        Vector3 worldPosition = transform.position; // D�nya pozisyonu
        Vector2 miniMapPosition = new Vector2(worldPosition.x, worldPosition.z); // 2D pozisyon

        // Mini haritay� doldurma
        miniMapIndicator.localPosition = new Vector3(miniMapPosition.x, miniMapPosition.y, 0);
    }
}

