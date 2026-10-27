using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify the password field, and save
        using (Document doc = new Document(inputPath))
        {
            const string fieldName = "PasswordField";
            const int maxLength = 20;

            // Retrieve the field from the form collection
            TextBoxField passwordField = doc.Form[fieldName] as TextBoxField;

            if (passwordField != null)
            {
                // -----------------------------------------------------------------
                // 1. Enforce maximum length (20 characters) via JavaScript action.
                //    Use a valid AnnotationActionCollection property – OnModifyCharacter.
                // -----------------------------------------------------------------
                string js = $"if (event.value.length > {maxLength}) {{ event.value = event.value.substring(0, {maxLength}); }}";
                passwordField.Actions.OnModifyCharacter = new JavascriptAction(js);

                // -----------------------------------------------------------------
                // 2. Set the field to behave as a password (mask input).
                //    The property may not exist in all versions, so we set it via reflection.
                // -----------------------------------------------------------------
                SetOptionalBooleanProperty(passwordField, "IsPassword", true);
            }
            else
            {
                Console.Error.WriteLine($"Password field '{fieldName}' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Sets a boolean property on an object using reflection if the property exists and is writable.
    /// </summary>
    private static void SetOptionalBooleanProperty(object obj, string propertyName, bool value)
    {
        PropertyInfo prop = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.CanWrite && (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?)))
        {
            prop.SetValue(obj, value);
        }
    }
}
