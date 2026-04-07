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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Ignore corrupted objects to prevent exceptions during copy/merge operations
                doc.IgnoreCorruptedObjects = true;

                // Open the JSON file once and reuse the stream for each field
                using (FileStream jsonStream = File.OpenRead(jsonPath))
                {
                    // Iterate over all form fields in the document
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Reset stream position before each import attempt
                        jsonStream.Position = 0;

                        // Attempt to import the field value from JSON.
                        // ImportValueFromJson returns false when the field name is not present.
                        bool imported = field.ImportValueFromJson(jsonStream);
                        if (!imported)
                        {
                            // Gracefully skip missing fields without throwing an exception
                            Console.WriteLine($"Field '{field.FullName}' not found in JSON – leaving unchanged.");
                        }
                    }
                }

                // Save the filled PDF to the output path
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bulk fill completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // General error handling – logs any unexpected issues
            Console.Error.WriteLine($"Error during bulk fill: {ex.Message}");
        }
    }
}