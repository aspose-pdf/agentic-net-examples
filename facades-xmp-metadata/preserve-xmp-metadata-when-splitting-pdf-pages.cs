using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Retrieve XMP metadata from the original PDF
        // ------------------------------------------------------------
        byte[] xmpData;
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPath);
            xmpData = xmp.GetXmpMetadata(); // XML bytes
        }

        // ------------------------------------------------------------
        // Split the source PDF into single‑page streams
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPath);

        // ------------------------------------------------------------
        // For each page stream, load it as a Document, attach the XMP,
        // and save to a separate file.
        // ------------------------------------------------------------
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Reset stream position before reading
            pageStreams[i].Position = 0;

            using (Document pageDoc = new Document(pageStreams[i]))
            {
                // Apply the original XMP metadata
                using (MemoryStream xmpStream = new MemoryStream(xmpData))
                {
                    pageDoc.SetXmpMetadata(xmpStream);
                }

                string outPath = Path.Combine(outputDir, $"page{i + 1}.pdf");
                pageDoc.Save(outPath);
                Console.WriteLine($"Saved page {i + 1} → {outPath}");
            }

            // Release the memory stream for the current page
            pageStreams[i].Dispose();
        }
    }
}