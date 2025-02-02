using UnityEditor;


//Bu kodlarý oyun yaparken scriptler yerine eventler üzerinde çalýþarak basitliði arttýrmak için uyguladým.
[CustomEditor(typeof(Interactable),true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            //Burada eventler üzerinde iþlem yaparken kodlara ihtiyaç duyulmamasý için kullanýcý uyaran bir sistem yazýyoruz.
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message",interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents.",MessageType.Info);

            //GameObject hala o componenti almadýysa diye de uyarý veriyoruz.
            if (interactable.GetComponent<InteractionEvent>() == null) 
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            if (interactable.useEvents)
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                    interactable.gameObject.AddComponent<InteractionEvent>();

            }
            else
            {

                if (interactable.GetComponent<InteractionEvent>() != null)
                {
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }
            }
        }
    }
}
