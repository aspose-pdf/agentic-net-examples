using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string jsonPath      = "appearance.json";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the target PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Import form fields (including appearance settings) from the JSON file
                doc.Form.ImportFromJson(jsonPath);

                // Save the updated PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF with imported appearance saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}