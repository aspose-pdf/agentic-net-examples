using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "readonly_form.pdf";
        const string fieldName = "CustomerName"; // name of the form field to lock

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to a Field.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Mark the field as read‑only to prevent further editing.
            field.ReadOnly = true;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field '{fieldName}' set to read‑only and saved to '{outputPath}'.");
    }
}
