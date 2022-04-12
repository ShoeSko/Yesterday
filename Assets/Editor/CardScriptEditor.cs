using UnityEditor;

[CustomEditor(typeof(CardScript))]
public class CardScriptEditor : Editor
{
    // The various categories the editor will display the variables in
    public enum DisplayCategory
    {
        Card, Base, Shooting, Punching, Special, Bestiary
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
            case DisplayCategory.Card:
                DisplayCardInfo();
                break;

            case DisplayCategory.Base:
                DisplayBaseInfo();
                break;

            case DisplayCategory.Shooting:
                DisplayShootingInfo();
                break;

            case DisplayCategory.Punching:
                DisplayPunchingInfo();
                break;

            case DisplayCategory.Special:
                DisplaySpecialInfo();
                break;

            case DisplayCategory.Bestiary:
                DisplayBestiaryInfo();
                break;

        }

        // Save all changes made on the Inspector
        serializedObject.ApplyModifiedProperties();
    }

    void DisplayCardInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cardName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("image"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("backgroundImage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("manaCost"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("effectDescription"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("effectDescription2"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("effectDescription3"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("flavorTxt"));
    }


    // When the categoryToDisplay enum is at "Basic"
    void DisplayBaseInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Prefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("health"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damageType"));
    }

    void DisplayShootingInfo()
    {
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("IsShooter"));


        // Store the IsShooter bool as a serializedProperty so we can access it
        SerializedProperty IsShooter = serializedObject.FindProperty("isShooter");

        // Draw a property for the IsShooter bool
        EditorGUILayout.PropertyField(IsShooter);

        // Check if IsShooter is true
        if (IsShooter.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("shootRechargeTime"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("projectileSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("projectilePrefab"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("projectileDamage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("shootingTargetLayer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("edgeOfRangeLayer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("targetsToShoot"));
        }
    }

    void DisplayPunchingInfo()
    {
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("IsPunching"));


        // Store the IsShooter bool as a serializedProperty so we can access it
        SerializedProperty IsPunching = serializedObject.FindProperty("isPunching");

        // Draw a property for the IsShooter bool
        EditorGUILayout.PropertyField(IsPunching);

        // Check if IsShooter is true
        if (IsPunching.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("punchRechargeTime"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("punchDamage"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("punchingTargetLayer"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("targetsToPunch"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("canPunchEverything"));
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("HasKnockback"));

            // Store the IsShooter bool as a serializedProperty so we can access it
            SerializedProperty HasKnockback = serializedObject.FindProperty("hasKnockback");

            // Draw a property for the IsShooter bool
            EditorGUILayout.PropertyField(HasKnockback);

            // Check if IsShooter is true
            if (HasKnockback.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("knockbackPower"));
            }

        }
    }

    void DisplaySpecialInfo()
    {
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("IsSpecial"));


        // Store the IsShooter bool as a serializedProperty so we can access it
        SerializedProperty IsSpecial = serializedObject.FindProperty("isSpecial");

        // Draw a property for the IsShooter bool
        EditorGUILayout.PropertyField(IsSpecial);

        // Check if IsShooter is true
        if (IsSpecial.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("isSacrificialKill"));
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("IsSupportExpert"));

            // Store the IsShooter bool as a serializedProperty so we can access it
            SerializedProperty IsSupportExpert = serializedObject.FindProperty("isSupportExpert");

            // Draw a property for the IsShooter bool
            EditorGUILayout.PropertyField(IsSupportExpert);

            // Check if IsShooter is true
            if (IsSupportExpert.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("allyLayerToTarget"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("hitAllyRange"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("sizeBuff"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("healthBuff"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("damageBuff"));

            }
        }
    }

    void DisplayBestiaryInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("unitIndex"));
    }

}