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
            // Load the PDF template
            using (Document doc = new Document(pdfPath))
            {
                // Read JSON once and keep it in memory for repeated use
                string jsonContent = File.ReadAllText(jsonPath);
                byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonContent);

                // Iterate over each form field and attempt to import its value
                foreach (Field field in doc.Form.Fields)
                {
                    // Create a fresh stream for each import attempt
                    using (MemoryStream jsonStream = new MemoryStream(jsonBytes))
                    {
                        bool imported = field.ImportValueFromJson(jsonStream);
                        if (!imported)
                        {
                            // Field not present in JSON – skip gracefully
                            Console.WriteLine($"Field '{field.FullName}' not found in JSON, skipping.");
                        }
                    }
                }

                // Save the filled PDF
                doc.Save(outputPath);
                Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during bulk fill: {ex.Message}");
        }
    }
}