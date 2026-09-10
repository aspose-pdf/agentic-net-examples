using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonPath = "appearance.json";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
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
            using (Document doc = new Document(inputPdf))
            {
                // Import appearance settings from the JSON file into the form fields
                doc.Form.ImportFromJson(jsonPath);

                // Save the updated PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF saved with imported appearances: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}