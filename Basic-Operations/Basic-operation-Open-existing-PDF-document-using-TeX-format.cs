using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Paths – adjust as needed
        const string texFilePath = "input.tex";
        const string outputPdfPath = "output.pdf";

        // Verify the TeX source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"TeX file not found: {texFilePath}");
            return;
        }

        try
        {
            // Initialize loading options for TeX files (default settings)
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();

            // Load the TeX file and convert it to a PDF document
            Document pdfDocument = new Document(texFilePath, texLoadOptions);

            // Save the resulting PDF
            pdfDocument.Save(outputPdfPath);
            
            Console.WriteLine($"PDF successfully created at: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}