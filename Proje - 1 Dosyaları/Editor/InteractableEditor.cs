using UnityEditor;


//Bu kodlar� oyun yaparken scriptler yerine eventler �zerinde �al��arak basitli�i artt�rmak i�in uygulad�m.
[CustomEditor(typeof(Interactable),true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            //Burada eventler �zerinde i�lem yaparken kodlara ihtiya� duyulmamas� i�in kullan�c� uyaran bir sistem yaz�yoruz.
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message",interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents.",MessageType.Info);

            //GameObject hala o componenti almad�ysa diye de uyar� veriyoruz.
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
