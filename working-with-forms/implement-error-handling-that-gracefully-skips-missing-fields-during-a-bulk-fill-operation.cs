using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "template.pdf";
        const string jsonPath     = "data.json";
        const string outputPdfPath = "filled.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonPath}");
            return;
        }

        // Load JSON data into memory once – will be reused for each field
        byte[] jsonBytes = File.ReadAllBytes(jsonPath);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in pdfDoc.Form.Fields)
            {
                try
                {
                    // Create a fresh MemoryStream for each import operation
                    using (MemoryStream jsonStream = new MemoryStream(jsonBytes))
                    {
                        // ImportValueFromJson returns true if the field was found in the JSON
                        bool imported = field.ImportValueFromJson(jsonStream);

                        if (!imported)
                        {
                            // Field missing in JSON – skip gracefully
                            Console.WriteLine($"Field '{field.FullName}' not present in JSON, skipping.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error but continue processing remaining fields
                    Console.Error.WriteLine($"Error processing field '{field.FullName}': {ex.Message}");
                }
            }

            // Save the filled PDF document
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Bulk fill completed. Output saved to '{outputPdfPath}'.");
    }
}