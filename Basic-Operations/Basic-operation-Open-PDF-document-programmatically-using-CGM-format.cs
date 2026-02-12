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
            // Load the CGM file into a PDF document using default load options
            // (Document constructor with file name and LoadOptions)
            Document pdfDocument = new Document(cgmPath, new CgmLoadOptions());

            // Save the resulting PDF document
            // document-save rule
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"Successfully converted '{cgmPath}' to '{pdfPath}'.");
        }
        catch (InvalidCgmFileFormatException ex)
        {
            Console.Error.WriteLine($"Invalid CGM file format: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}