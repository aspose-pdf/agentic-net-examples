using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // Path to the PDF containing the form
        const string fieldName = "MyField";     // Name of the field to retrieve

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field.
            Field? formField = doc.Form[fieldName] as Field;
            if (formField != null)
            {
                // Example: output the current value of the field
                Console.WriteLine($"Field \"{fieldName}\" value: {formField.Value}");
            }
            else
            {
                Console.WriteLine($"Field \"{fieldName}\" was not found or is not a form field.");
            }

            // No changes are made, but if you need to persist modifications, call Save here
            // doc.Save("output.pdf");
        }
    }
}
