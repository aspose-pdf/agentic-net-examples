using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string tempXmlPath = "temp.xml";
        const string outputPdfPath = "compressed_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // STEP 1: Load the original PDF and save it as XML
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            var xmlOpts = new XmlSaveOptions();
            pdfDoc.Save(tempXmlPath, xmlOpts);
        }

        // STEP 2: Reload the document from the XML representation and compress content streams
        using (Document xmlDoc = new Document(tempXmlPath))
        {
            // Use OptimizationOptions (Flate compression is the default for streams)
            var opt = OptimizationOptions.All();
            opt.CompressObjects = true;            // compress object streams
            opt.RemoveUnusedObjects = true;        // clean up unused objects
            opt.RemoveUnusedStreams = true;        // clean up unused streams
            opt.SubsetFonts = true;                // subset fonts to reduce size
            opt.LinkDuplicateStreams = true;       // reuse identical streams

            xmlDoc.OptimizeResources(opt);

            // Save the optimized PDF
            var saveOpts = new PdfSaveOptions();
            xmlDoc.Save(outputPdfPath, saveOpts);
        }

        // Cleanup temporary XML file
        try { File.Delete(tempXmlPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Compressed PDF saved to '{outputPdfPath}'.");
    }
}