using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";
        const int maxLength = 20;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Ensure the form and the specific field exist
                if (doc.Form != null && doc.Form[fieldName] is TextBoxField textBox)
                {
                    // Attempt to set the MaxLength property via reflection (available in newer versions)
                    PropertyInfo maxLenProp = textBox.GetType().GetProperty("MaxLength", BindingFlags.Public | BindingFlags.Instance);
                    if (maxLenProp != null && maxLenProp.CanWrite)
                    {
                        maxLenProp.SetValue(textBox, maxLength);
                    }
                    else
                    {
                        // Fallback: enforce length with JavaScript on validation (supported across versions)
                        string js = $"if (event.value.length > {maxLength}) event.value = event.value.substring(0, {maxLength});";
                        // The Actions collection on a form field provides OnValidate (executed when the field loses focus)
                        textBox.Actions.OnValidate = new JavascriptAction(js);
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Text box field '{fieldName}' not found.");
                }

                doc.Save(outputPath);
            }

            Console.WriteLine($"Form field '{fieldName}' max length set to {maxLength} and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
