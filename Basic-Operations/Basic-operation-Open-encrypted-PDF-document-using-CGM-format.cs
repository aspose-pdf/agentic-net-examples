using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main(string[] args)
    {
        // Input CGM file path (the source graphic metafile)
        const string inputCgmPath = "input.cgm";
        // Output PDF file path (the resulting PDF document)
        const string outputPdfPath = "output.pdf";

        // Verify that the CGM file exists before attempting to load it
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{inputCgmPath}'.");
            return;
        }

        try
        {
            // Create default load options for CGM import.
            // CgmLoadOptions resides in the Aspose.Pdf namespace, so no extra using directive is required.
            var loadOptions = new CgmLoadOptions();

            // Load the CGM file and convert it to a PDF document.
            // The Document constructor that accepts (string, LoadOptions) is used.
            var pdfDocument = new Document(inputCgmPath, loadOptions);

            // Save the resulting PDF document to the specified output path.
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"CGM file successfully converted and saved as PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., invalid CGM format, I/O issues)
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}