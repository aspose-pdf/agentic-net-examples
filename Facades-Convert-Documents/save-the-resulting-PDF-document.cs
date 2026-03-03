using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfViewer facade, bind the source PDF, and save the result.
        PdfViewer viewer = new PdfViewer();
        try
        {
            // Load the PDF document into the facade.
            viewer.BindPdf(inputPath);

            // Example operation: set zoom (optional).
            // viewer.Zoom = 1.0f;

            // Save the resulting PDF to the specified file.
            viewer.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released.
            viewer.Close();
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}