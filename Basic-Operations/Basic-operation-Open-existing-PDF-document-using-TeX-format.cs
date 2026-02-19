using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the input TeX file
        string texFilePath = @"input.tex";

        // Path to the output PDF file
        string pdfOutputPath = @"output.pdf";

        // Verify that the TeX source file exists
        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"Error: TeX file not found at '{texFilePath}'.");
            return;
        }

        try
        {
            // Initialize TeX load options (default settings)
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();

            // Load the TeX file and create a PDF document
            Document pdfDocument = new Document(texFilePath, texLoadOptions);

            // Save the generated PDF document
            // (uses the provided document-save rule)
            pdfDocument.Save(pdfOutputPath);

            Console.WriteLine($"PDF successfully created at '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}