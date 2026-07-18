using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "target.pdf";
        const string jsonPath = "appearance.json";
        const string outputPath = "output.pdf";

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
            // Load the target PDF.
            using (Document doc = new Document(pdfPath))
            {
                // Import appearance settings (and field values) from the JSON file.
                doc.Form.ImportFromJson(jsonPath);

                // Save the PDF with the imported appearances.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with imported appearances: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}