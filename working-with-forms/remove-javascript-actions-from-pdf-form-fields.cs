using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields (WidgetAnnotation objects)
            foreach (WidgetAnnotation field in doc.Form)
            {
                // Remove any JavaScript actions attached to the field
                if (field.Actions != null)
                {
                    // The AnnotationActionCollection does not expose a generic "JavaScript" property.
                    // Valid action properties are those that return a JavascriptAction (e.g., OnEnter, OnExit, OnPressMouseBtn, etc.).
                    // We clear all of them via reflection to ensure every possible JavaScript action is removed.
                    PropertyInfo[] actionProps = field.Actions.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in actionProps)
                    {
                        if (prop.PropertyType == typeof(JavascriptAction) && prop.CanWrite)
                        {
                            prop.SetValue(field.Actions, null);
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript actions removed. Saved to '{outputPath}'.");
    }
}
