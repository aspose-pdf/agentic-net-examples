using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "target.pdf";          // Input PDF with form fields
        const string jsonPath = "appearance.json";    // JSON containing appearance settings
        const string outputPath = "target_with_appearance.pdf";

        // Verify input files exist
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
            // Load the target PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Open the JSON file as a stream and import appearance settings
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                {
                    // Import form fields (including appearance) from the JSON stream
                    doc.Form.ImportFromJson(jsonStream);
                }

                // Save the updated PDF with the imported appearances
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with imported appearances to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}