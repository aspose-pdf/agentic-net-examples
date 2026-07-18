using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PreserveXmpDuringSplit
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source multi‑page PDF
        const string outputFolder = "SplitPages";              // folder for split files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // ---------- Extract XMP metadata from the original document ----------
        byte[] xmpBytes;
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdfPath);
            xmpBytes = xmp.GetXmpMetadata();   // XML representation of XMP metadata
        }

        // ---------- Split the PDF into single‑page streams ----------
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdfPath);

        // ---------- Create a PDF for each page, attach the XMP metadata and save ----------
        for (int i = 0; i < pageStreams.Length; i++)
        {
            MemoryStream pageStream = pageStreams[i];
            pageStream.Position = 0;   // reset to beginning before loading

            using (Document pageDoc = new Document(pageStream))
            {
                // Attach the previously extracted XMP metadata
                using (MemoryStream metaStream = new MemoryStream(xmpBytes))
                {
                    pageDoc.SetXmpMetadata(metaStream);
                }

                // Save the single‑page PDF
                string outputPath = Path.Combine(outputFolder, $"page{i + 1}.pdf");
                pageDoc.Save(outputPath);
                Console.WriteLine($"Saved: {outputPath}");
            }

            // Dispose the stream that held the page data
            pageStream.Dispose();
        }
    }
}