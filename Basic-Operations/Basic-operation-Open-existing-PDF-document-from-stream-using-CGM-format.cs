using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the input CGM file and the output PDF file
        const string inputCgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";

        // Verify that the CGM file exists before attempting to open it
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{inputCgmPath}'.");
            return;
        }

        try
        {
            // Open the CGM file as a stream
            using (FileStream cgmStream = new FileStream(inputCgmPath, FileMode.Open, FileAccess.Read))
            {
                // Create load options for CGM import (default A4 page size, 300 DPI)
                CgmLoadOptions loadOptions = new CgmLoadOptions();

                // Load the CGM content into an Aspose.Pdf Document using the stream and load options
                Document pdfDocument = new Document(cgmStream, loadOptions);

                // Save the resulting PDF document to the specified output path
                pdfDocument.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully converted '{inputCgmPath}' to PDF and saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., invalid CGM format, I/O issues)
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
