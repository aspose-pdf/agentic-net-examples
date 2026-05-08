using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // source multi‑page PDF
        const string outputFolder = "SplitPages";          // folder for single‑page PDFs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // 1. Extract the original XMP metadata (as raw XML bytes)
        byte[] xmpData;
        {
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(inputPdf);
            xmpData = xmp.GetXmpMetadata();               // raw XML representation
        }

        // 2. Split the source PDF into individual page streams
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

        // 3. For each page stream, load it into a Document, attach the XMP metadata,
        //    and save it as a separate PDF file.
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Reset stream position before loading
            pageStreams[i].Position = 0;

            using (Document pageDoc = new Document(pageStreams[i]))
            {
                // Attach the previously extracted XMP metadata
                using (MemoryStream xmpStream = new MemoryStream(xmpData))
                {
                    pageDoc.SetXmpMetadata(xmpStream);
                }

                // Build output file name (e.g., "page1.pdf", "page2.pdf", ...)
                string outPath = Path.Combine(outputFolder, $"page{i + 1}.pdf");

                // Save the single‑page PDF (preserving metadata)
                pageDoc.Save(outPath);
                Console.WriteLine($"Saved page {i + 1} → {outPath}");
            }

            // Dispose the memory stream for the current page
            pageStreams[i].Dispose();
        }
    }
}