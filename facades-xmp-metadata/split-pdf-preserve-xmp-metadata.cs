using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPattern = "output_page{0}.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Extract XMP metadata from the source PDF
        PdfXmpMetadata xmpMeta = new PdfXmpMetadata();
        xmpMeta.BindPdf(inputPdf);
        byte[] xmpBytes = xmpMeta.GetXmpMetadata();

        // Split the source PDF into individual page streams
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

        // Process each page stream: attach original XMP metadata and save to file
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Ensure the stream is positioned at the beginning
            pageStreams[i].Position = 0;

            string outputPath = string.Format(outputPattern, i + 1);

            using (Document pageDoc = new Document(pageStreams[i]))
            {
                // Attach the original XMP metadata
                using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
                {
                    pageDoc.SetXmpMetadata(xmpStream);
                }

                // Save the single‑page PDF
                pageDoc.Save(outputPath);
            }

            // Dispose the page stream after use
            pageStreams[i].Dispose();
        }

        Console.WriteLine("Splitting completed. Individual pages saved with original XMP metadata.");
    }
}