using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ----- Form processing (example) -----
                // Fill a text field named "Name" if it exists
                // The indexer returns a WidgetAnnotation; cast to Field (or a concrete field type) to access the Value property.
                Field nameField = doc.Form["Name"] as Field;
                if (nameField != null)
                {
                    nameField.Value = "John Doe";
                }

                // ----- Set PDF metadata -----
                // Set author and title via the DocumentInfo properties
                doc.Info.Author = "Jane Smith";
                doc.Info.Title  = "Completed Form Document";

                // Save the modified PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved with updated metadata to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
