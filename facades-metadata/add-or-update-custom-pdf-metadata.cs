using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists; create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            var doc = new Document();
            doc.Pages.Add(); // add a blank page so the file is a valid PDF
            doc.Save(inputPath);
        }

        // Initialise PdfFileInfo with the existing document using the constructor that accepts a file path.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Add or update the custom metadata field "Version" while preserving existing entries.
            pdfInfo.SetMetaInfo("Version", "1.0");

            // Persist the modified metadata to a new file while preserving the original content.
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}