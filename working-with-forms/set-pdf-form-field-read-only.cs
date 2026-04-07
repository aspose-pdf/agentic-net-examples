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
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation. Cast it to a Field to access form‑specific members.
            Field? field = doc.Form[fieldName] as Field;
            if (field != null)
            {
                // Mark the field as read‑only to prevent further editing.
                field.ReadOnly = true;
            }
            else
            {
                Console.WriteLine($"Form field '{fieldName}' not found or is not a standard form field.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only field to '{outputPath}'.");
    }
}
