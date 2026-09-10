using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "form.pdf";
        const string jsonPath  = "data.json";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPdf))
            {
                // Optional: ignore corrupted objects during processing
                doc.IgnoreCorruptedObjects = true;

                // Open the JSON file once and reuse the stream for each field
                using (FileStream jsonStream = File.OpenRead(jsonPath))
                {
                    // Iterate over all form fields in the document
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Reset stream position before each import attempt
                        jsonStream.Position = 0;

                        // Try to import the field value from JSON.
                        // Returns false if the field name is not present in the JSON.
                        bool imported = field.ImportValueFromJson(jsonStream);

                        if (!imported)
                        {
                            // Gracefully skip missing fields
                            Console.WriteLine($"Field '{field.FullName}' not found in JSON – skipping.");
                        }
                        else
                        {
                            Console.WriteLine($"Field '{field.FullName}' filled successfully.");
                        }
                    }
                }

                // Save the filled PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Bulk fill completed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during bulk fill: {ex.Message}");
        }
    }
}