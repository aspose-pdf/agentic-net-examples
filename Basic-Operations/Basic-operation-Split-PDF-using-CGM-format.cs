using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input CGM file path
        const string cgmPath = "input.cgm";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the CGM source file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{cgmPath}'.");
            return;
        }

        try
        {
            // Load the CGM file into a PDF document using default options
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            Document pdfDocument = new Document(cgmPath, loadOptions);

            // Save the resulting PDF document
            pdfDocument.Save(pdfPath); // document-save rule

            Console.WriteLine($"CGM file successfully converted and saved as PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}