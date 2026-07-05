using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "SplitPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // ----- Preserve XMP metadata -----
        // Load XMP metadata from the source PDF
        PdfXmpMetadata xmpMeta = new PdfXmpMetadata();
        xmpMeta.BindPdf(inputPdf);
        byte[] xmpBytes = xmpMeta.GetXmpMetadata();

        // ----- Split PDF into single‑page streams -----
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

        // Process each page stream
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Use a using block for the MemoryStream to ensure it is disposed
            using (MemoryStream pageStream = pageStreams[i])
            {
                // Load the single‑page PDF into a Document
                using (Document pageDoc = new Document(pageStream))
                {
                    // Apply the original XMP metadata to the split document
                    using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
                    {
                        pageDoc.SetXmpMetadata(xmpStream);
                    }

                    // Save the split page to a file
                    string outPath = Path.Combine(outputFolder, $"page{i + 1}.pdf");
                    pageDoc.Save(outPath);
                    Console.WriteLine($"Saved page {i + 1} → {outPath}");
                }
            }
        }

        Console.WriteLine("Splitting completed with XMP metadata preserved.");
    }
}