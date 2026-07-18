using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "filled.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Optional: ignore corrupted objects during any copy operations
            doc.IgnoreCorruptedObjects = true;

            // Open the JSON stream once and reuse it for each field import
            using (FileStream jsonStream = File.OpenRead(jsonPath))
            {
                // Iterate over all form fields in the document
                foreach (Field field in doc.Form.Fields)
                {
                    try
                    {
                        // Attempt to import the value for the current field.
                        // ImportValueFromJson returns false when the field name is not present in the JSON.
                        bool imported = field.ImportValueFromJson(jsonStream);

                        // Reset the stream position for the next field import
                        jsonStream.Position = 0;

                        if (!imported)
                        {
                            // Field not found in JSON – skip gracefully
                            Console.WriteLine($"Field '{field.FullName}' not found in JSON. Skipping.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Any unexpected error while importing a specific field is logged,
                        // but the bulk operation continues with the next field.
                        Console.Error.WriteLine($"Error importing field '{field.FullName}': {ex.Message}");
                        // Ensure the stream is reset even after an exception
                        jsonStream.Position = 0;
                    }
                }
            }

            // Save the filled PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bulk fill completed. Output saved to '{outputPath}'.");
    }
}