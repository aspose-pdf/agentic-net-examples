using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PreserveXmpDuringSplit
{
    static void Main()
    {
        const string inputPdf = "input.pdf";                     // source multi‑page PDF
        const string outputFolder = "SplitPages";                // folder for split pages
        const string fileNameTemplate = "page%NUM%.pdf";         // %NUM% will be replaced by page number

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // 1. Retrieve the original XMP metadata (as raw XML bytes)
        // ------------------------------------------------------------
        byte[] xmpData;
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            xmp.BindPdf(inputPdf);
            xmpData = xmp.GetXmpMetadata();   // may be null/empty if no XMP present
        }

        // ------------------------------------------------------------
        // 2. Split the PDF into individual page streams
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdf); // returns one stream per page

        // ------------------------------------------------------------
        // 3. For each page stream, load it as a Document, attach the XMP,
        //    and save to a file whose name follows the template.
        // ------------------------------------------------------------
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Page numbers are 1‑based in the file name
            int pageNumber = i + 1;
            string outputPath = Path.Combine(
                outputFolder,
                fileNameTemplate.Replace("%NUM%", pageNumber.ToString())
            );

            using (MemoryStream pageStream = pageStreams[i])
            {
                pageStream.Position = 0; // reset before reading

                using (Document pageDoc = new Document(pageStream))
                {
                    // If original PDF had XMP metadata, re‑apply it
                    if (xmpData != null && xmpData.Length > 0)
                    {
                        using (MemoryStream xmpStream = new MemoryStream(xmpData))
                        {
                            pageDoc.SetXmpMetadata(xmpStream);
                        }
                    }

                    // Save the single‑page PDF
                    pageDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"Saved page {pageNumber} → {outputPath}");
        }

        Console.WriteLine("All pages split with original XMP metadata preserved.");
    }
}