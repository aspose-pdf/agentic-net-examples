using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for Border and BorderStyle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF containing the form field
        const string outputPath = "output.pdf"; // PDF after border style change
        const string fieldName = "myField";     // Name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the Document is disposed properly (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field before use
            Field? field = doc.Form[fieldName] as Field;
            if (field != null)
            {
                // Set a beveled border style (three‑dimensional appearance)
                // Border requires the owning annotation (field) in its constructor
                field.Border = new Border(field)
                {
                    Style = BorderStyle.Beveled,
                    Width = 1 // integer thickness
                };
                // Border colour is taken from the field's own Color property
                field.Color = Aspose.Pdf.Color.Black;
            }
            else
            {
                Console.WriteLine($"Form field '{fieldName}' not found or is not a field.");
            }

            // Save the modified PDF (save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
