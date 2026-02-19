using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM file path
        const string cgmPath = "input.cgm";
        // Output PDF file path
        const string pdfPath = "output.pdf";

        // Verify that the CGM file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{cgmPath}'.");
            return;
        }

        try
        {
            // Load the CGM file with default options (A4 page size, 300 DPI)
            var loadOptions = new CgmLoadOptions();
            Document pdfDocument = new Document(cgmPath, loadOptions);

            // Save the resulting PDF document
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"CGM file successfully converted and saved as PDF at '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}