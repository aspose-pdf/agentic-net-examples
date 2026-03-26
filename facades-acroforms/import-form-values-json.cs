using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string jsonPath = "data.json";
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

        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Bind the form facade to the loaded document
            Form pdfForm = new Form(pdfDocument);

            // Import field values from JSON; fields missing in the PDF are ignored automatically
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                pdfForm.ImportJson(jsonStream);
            }

            // Save the updated PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPdfPath}'.");
    }
}