using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonPath = "data.json";
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
            using (Document doc = new Document(inputPdf))
            {
                // Bulk import form fields from JSON file
                doc.Form.ImportFromJson(jsonPath);

                // Save the updated PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}