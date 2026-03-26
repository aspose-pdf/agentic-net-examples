using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";
        const string fieldName = "FirstName";
        const string fieldValue = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a Form object with input and output PDF files
        Form pdfForm = new Form(inputPath, outputPath);
        bool filled = pdfForm.FillField(fieldName, fieldValue);
        if (!filled)
        {
            Console.Error.WriteLine($"Field '{fieldName}' not found or could not be filled.");
        }

        // Save the updated PDF
        pdfForm.Save();
        Console.WriteLine($"Field '{fieldName}' filled and saved to '{outputPath}'.");
    }
}