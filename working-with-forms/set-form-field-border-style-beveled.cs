using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms; // <-- added namespace for form field classes

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF containing the form
        const string outputPath = "output.pdf";         // PDF with updated border style
        const string fieldName  = "myTextField";        // name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name; cast to the appropriate field type
            // (TextBoxField is used here as an example – adjust the cast for other field types)
            TextBoxField field = doc.Form[fieldName] as TextBoxField;

            if (field != null)
            {
                // Create a Border object linked to the field (requires the parent annotation)
                // and set the style to Beveled for a three‑dimensional appearance.
                field.Border = new Border(field)
                {
                    Style = BorderStyle.Beveled,
                    Width = 2 // optional: set border thickness
                };
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or not a TextBoxField.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}
