using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source CGM file and the desired PDF output
        const string cgmPath = "input.cgm";
        const string outputPdf = "output.pdf";

        // Verify that the CGM file exists before attempting to open it
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{cgmPath}'.");
            return;
        }

        try
        {
            // Open the CGM file as a read‑only stream
            using (FileStream cgmStream = new FileStream(cgmPath, FileMode.Open, FileAccess.Read))
            {
                // Create load options specific to CGM import (defaults to A4 @ 300 dpi)
                CgmLoadOptions loadOptions = new CgmLoadOptions();

                // Load the CGM data from the stream into an Aspose.Pdf Document
                Document pdfDocument = new Document(cgmStream, loadOptions);

                // Save the resulting PDF document to disk
                pdfDocument.Save(outputPdf);
            }

            Console.WriteLine($"PDF successfully created at '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors (e.g., invalid CGM format)
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}