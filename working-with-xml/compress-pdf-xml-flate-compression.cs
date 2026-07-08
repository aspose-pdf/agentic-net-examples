using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string intermediateXml = "temp.xml";
        const string outputPdf = "compressed_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // 1. Load the original PDF.
        using (Document pdfDoc = new Document(inputPdf))
        {
            // 2. Export the PDF to XML.
            XmlSaveOptions xmlSaveOpts = new XmlSaveOptions();
            pdfDoc.Save(intermediateXml, xmlSaveOpts);
        }

        // 3. Load the XML back into a PDF document.
        using (Document xmlDoc = new Document(intermediateXml, new XmlLoadOptions()))
        {
            // 4. Prepare optimization options: enable Flate compression of object streams.
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true // Packs objects into streams and applies Flate compression.
            };

            // 5. Apply the optimization to the document.
            xmlDoc.OptimizeResources(opt);

            // 6. Save the compressed PDF.
            xmlDoc.Save(outputPdf);
        }

        // Clean up the temporary XML file.
        try { File.Delete(intermediateXml); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Compressed PDF saved to '{outputPdf}'.");
    }
}
