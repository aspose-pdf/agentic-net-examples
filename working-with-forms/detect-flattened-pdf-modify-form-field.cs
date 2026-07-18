using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for Field access

class Program
{
    static void Main()
    {
        const string inputPath = "flattened.pdf";
        const string outputPath = "restored.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // If the PDF was previously flattened, the Form collection will be empty.
            // Flattening removes form fields permanently; they cannot be restored.
            // To edit the form you must reload the original, unflattened PDF.
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("Document is flattened; form fields are not present and cannot be restored.");
            }
            else
            {
                // Retrieve the field safely – the indexer returns a WidgetAnnotation, so cast to Field.
                Field? nameField = doc.Form["Name"] as Field;
                if (nameField != null)
                {
                    // Use the Field.Value property (not WidgetAnnotation.Value)
                    nameField.Value = "John Doe";
                }
                else
                {
                    Console.WriteLine("Field 'Name' not found or is not a form field.");
                }
            }

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved to '{outputPath}'.");
    }
}