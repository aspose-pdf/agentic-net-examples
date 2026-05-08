using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "readonly_output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field by name; returns null if the field does not exist
            var textField = doc.Form[fieldName] as TextBoxField;
            if (textField != null)
            {
                // Set an initial value
                textField.Value = "Initial value";

                // Mark the field as read‑only to prevent further editing
                textField.ReadOnly = true;
            }
            else
            {
                Console.WriteLine($"Form field '{fieldName}' not found or is not a text box.");
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Read‑only PDF saved to '{outputPath}'.");
    }
}
