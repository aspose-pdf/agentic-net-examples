using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input CGM file and output PDF paths.
        string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        string cgmPath = Path.Combine(dataDir, "sample.cgm");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the CGM file exists.
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{cgmPath}'.");
            return;
        }

        // Create load options for CGM conversion.
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        // Example: disable font license checks if needed.
        // loadOptions.DisableFontLicenseVerifications = true;

        // Load the CGM file into an Aspose.Pdf Document.
        Document pdfDocument = new Document(cgmPath, loadOptions);

        // Save the document as a regular PDF (non‑PDF/A).
        pdfDocument.Save(pdfPath);

        Console.WriteLine($"CGM file successfully converted to PDF: {pdfPath}");
    }
}