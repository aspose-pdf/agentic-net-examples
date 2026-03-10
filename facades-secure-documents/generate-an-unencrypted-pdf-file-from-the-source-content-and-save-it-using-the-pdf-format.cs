using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source content (must be an existing PDF file)
        const string sourcePath = "source.pdf";
        // Path where the unencrypted PDF will be saved
        const string outputPath = "output.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create the PdfViewer facade (provided create rule)
        PdfViewer viewer = new PdfViewer();

        try
        {
            // Load the source PDF into the facade (provided load rule)
            viewer.BindPdf(sourcePath);

            // Save the result as an unencrypted PDF (provided save rule)
            viewer.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released
            viewer.Close();
        }

        Console.WriteLine($"Unencrypted PDF saved to '{outputPath}'.");
    }
}