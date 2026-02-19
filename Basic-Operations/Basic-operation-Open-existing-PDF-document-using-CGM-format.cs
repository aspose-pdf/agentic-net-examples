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
            // Load the CGM file into a PDF Document using default load options
            // Document(string, LoadOptions) constructor is used as per Aspose.Pdf API
            Document pdfDocument = new Document(cgmPath, new CgmLoadOptions());

            // Save the resulting PDF document
            // Using the provided document-save rule
            pdfDocument.Save(pdfPath);

            Console.WriteLine($"CGM file successfully converted and saved as PDF: {pdfPath}");
        }
        catch (InvalidCgmFileFormatException ex)
        {
            Console.Error.WriteLine($"Invalid CGM file format: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}