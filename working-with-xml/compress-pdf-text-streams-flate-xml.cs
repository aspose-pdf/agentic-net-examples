using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string tempXml   = "temp.xml";
        const string outputPdf = "compressed.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the original PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Convert PDF to XML (intermediate representation) using XmlSaveOptions
            pdfDoc.Save(tempXml, new XmlSaveOptions());

            // Load the PDF back from the XML representation
            using (Document xmlDoc = new Document(tempXml))
            {
                // Optimize resources – this applies Flate compression to text streams
                // and removes unused objects/streams, effectively reducing file size.
                OptimizationOptions opt = OptimizationOptions.All();
                opt.RemoveUnusedObjects = true;
                opt.RemoveUnusedStreams = true;
                // Images are already compressed; we keep image settings unchanged.
                xmlDoc.OptimizeResources(opt);

                // Save the compressed PDF
                xmlDoc.Save(outputPdf);
            }
        }

        // Clean up the temporary XML file
        try { File.Delete(tempXml); } catch { }

        Console.WriteLine($"Compressed PDF saved to '{outputPdf}'.");
    }
}
