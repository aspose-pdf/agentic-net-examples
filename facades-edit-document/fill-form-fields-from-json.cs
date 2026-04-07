using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "filled.pdf";
        const string jsonPath = "data.json";

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

        // Initialize the Form facade with source and destination PDF files
        Form pdfForm = new Form(inputPdf, outputPdf);

        // Import field values from the JSON file
        using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
        {
            pdfForm.ImportJson(jsonStream);
        }

        // Save the updated PDF
        pdfForm.Save();

        Console.WriteLine($"Form fields have been filled and saved to '{outputPdf}'.");
    }
}
