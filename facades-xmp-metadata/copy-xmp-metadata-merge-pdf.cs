using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string targetPath = "target.pdf";
        const string outputPath = "merged.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        // Extract XMP metadata from the source PDF
        PdfXmpMetadata sourceXmp = new PdfXmpMetadata();
        sourceXmp.BindPdf(sourcePath);
        byte[] xmpBytes = sourceXmp.GetXmpMetadata();

        // Load the target PDF, apply the extracted XMP metadata, and merge pages
        using (Document targetDoc = new Document(targetPath))
        {
            using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
            {
                targetDoc.SetXmpMetadata(xmpStream);
            }

            using (Document sourceDoc = new Document(sourcePath))
            {
                targetDoc.Pages.Add(sourceDoc.Pages);
            }

            targetDoc.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}
