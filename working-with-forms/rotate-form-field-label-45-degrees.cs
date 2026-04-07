using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_label.pdf";
        const string fieldName = "myField"; // replace with the actual form field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation. Cast it to Aspose.Pdf.Forms.Field
            // (or use the 'as' operator) before accessing field‑specific members.
            Field? field = doc.Form[fieldName] as Field;
            if (field != null)
            {
                // Rotate the field's rectangle by 45 degrees. Rectangle.Rotate expects degrees.
                field.Rect.Rotate(45);
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a form field.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated label PDF saved to '{outputPath}'.");
    }
}
