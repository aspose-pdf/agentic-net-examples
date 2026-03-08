using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";
        const string destPath   = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create a PdfViewer facade (create step)
        PdfViewer viewer = new PdfViewer();

        try
        {
            // Load the PDF into the facade (load step)
            viewer.BindPdf(sourcePath);

            // Perform any required operations on the viewer here
            // ...

            // Persist the PDF to the desired location (save step)
            viewer.Save(destPath);
        }
        finally
        {
            // Ensure resources are released
            viewer.Close();
        }

        Console.WriteLine($"PDF successfully saved to '{destPath}'.");
    }
}