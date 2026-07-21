using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";   // PDF with desired XMP metadata
        const string targetPath = "target.pdf";   // PDF that will receive the metadata
        const string outputPath = "merged.pdf";   // Resulting PDF after merge

        if (!File.Exists(sourcePath) || !File.Exists(targetPath))
        {
            Console.Error.WriteLine("Source or target PDF not found.");
            return;
        }

        try
        {
            // Load source and target documents
            using (Document sourceDoc = new Document(sourcePath))
            using (Document targetDoc = new Document(targetPath))
            {
                // Extract XMP metadata from source PDF
                PdfXmpMetadata xmpFacade = new PdfXmpMetadata();
                xmpFacade.BindPdf(sourcePath);
                byte[] xmpBytes = xmpFacade.GetXmpMetadata();

                // Apply extracted XMP metadata to target PDF
                using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
                {
                    targetDoc.SetXmpMetadata(xmpStream);
                }

                // Append all pages from source PDF to target PDF
                targetDoc.Pages.Add(sourceDoc.Pages);

                // Save the merged document with the transferred metadata
                targetDoc.Save(outputPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}