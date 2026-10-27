using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;   // Form field types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "CustomerID";          // name of the form field to modify
        const string tooltip    = "Enter a 10‑digit numeric ID (e.g., 1234567890)";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains AcroForm fields
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.Error.WriteLine("No form fields found in the document.");
                return;
            }

            // Find the field by its name
            TextBoxField field = doc.Form[fieldName] as TextBoxField;
            if (field == null)
            {
                Console.Error.WriteLine($"TextBoxField named '{fieldName}' not found.");
                return;
            }

            // Set the tooltip that appears when the user hovers over the field.
            // In Aspose.Pdf the tooltip is stored in the 'Contents' property of the widget annotation.
            field.Contents = tooltip;

            // Optionally, you can also set a default value or appearance here
            // field.Value = "";
            // field.Border = new Border(field) { Width = 1, Color = Aspose.Pdf.Color.Black };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tooltip added and saved to '{outputPath}'.");
    }
}
