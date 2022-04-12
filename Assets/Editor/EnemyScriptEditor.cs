using UnityEditor;

[CustomEditor(typeof(EnemyScript))]
public class EnemyScriptEditor : Editor
{
    // The various categories the editor will display the variables in
    public enum DisplayCategory
    {
        Base, Attack, Bestiary
    }
    // The enum field that will determine what variables to display in the Inspector
    public DisplayCategory categoryToDisplay;


    // The function that makes the custom editor work
    public override void OnInspectorGUI()
    {
        // Display the enum popup in the inspector
        categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Display", categoryToDisplay);

        // Create a space to separate this enum popup from the other variables 
        EditorGUILayout.Space();

        //Switch statement to handle what happens for each category
        switch (categoryToDisplay)
        {
            case DisplayCategory.Base:
                DisplayBaseInfo();
                break;

            case DisplayCategory.Attack:
                DisplayAttackInfo();
                break;

            case DisplayCategory.Bestiary:
                DisplayBestiaryInfo();
                break;

        }

        // Save all changes made on the Inspector
        serializedObject.ApplyModifiedProperties();
    }

    // When the categoryToDisplay enum is at "Basic"
    void DisplayBaseInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("moveSpeed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("obstacleTags"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("projectileTags"));
    }

    void DisplayAttackInfo()
    {
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("IsShooter"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackDamage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("attackSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("whatIsUnitLayer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damageType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("specialAnimationCheckList"));
    }

    void DisplayBestiaryInfo()
    {
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("bestiaryType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isBeast"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isHumanoid"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isMonstrosity"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyIndex"));
    }

}