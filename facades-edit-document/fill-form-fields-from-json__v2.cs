using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string jsonDataPath = "data.json";
        const string outputPdfPath = "filled.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        using (Document pdfDocument = new Document(inputPdfPath))
        {
            using (FileStream jsonStream = new FileStream(jsonDataPath, FileMode.Open, FileAccess.Read))
            {
                // Import field values from the JSON stream into the PDF form
                pdfDocument.Form.ImportFromJson(jsonStream);
            }

            // Save the updated PDF with filled form fields
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields imported from JSON and saved to '{outputPdfPath}'.");
    }
}
