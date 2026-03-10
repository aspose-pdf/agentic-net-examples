using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Output file path (PDF – PostScript output is not supported by Aspose.Pdf)
        const string outputPath = "output.pdf";

        // Create a simple PDF document (required as a source for PdfFileInfo)
        using (Document doc = new Document())
        {
            // Add a blank page so the document is not empty
            doc.Pages.Add();

            // Use PdfFileInfo facade to set metadata fields
            using (PdfFileInfo info = new PdfFileInfo(doc))
            {
                // Assign metadata
                info.Title    = "Sample Title";
                info.Author   = "John Doe";
                info.Subject  = "Demonstration of metadata assignment";
                info.Keywords = "Aspose.Pdf, Metadata, Example";

                // Save the updated document with the new metadata
                // SaveNewInfo writes the changes to a new file
                info.SaveNewInfo(outputPath);
            }
        }

        Console.WriteLine($"PDF with metadata saved to '{outputPath}'.");
        // Note: Aspose.Pdf does not support saving directly to PostScript format.
        // The generated PDF can be converted to PostScript using external tools if needed.
    }
}