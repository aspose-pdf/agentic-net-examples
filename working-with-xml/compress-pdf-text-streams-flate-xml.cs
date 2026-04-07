using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string tempXmlPath   = "temp.xml";
        const string outputPdfPath = "compressed_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the original PDF, export it to XML, then reload the XML.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export PDF content to XML representation.
            pdfDoc.SaveXml(tempXmlPath);

            // Reload the document from the generated XML.
            using (Document xmlDoc = new Document(tempXmlPath, new XmlLoadOptions()))
            {
                // Set optimization options to compress all objects (including text streams) using Flate.
                OptimizationOptions opt = new OptimizationOptions
                {
                    CompressObjects = true
                };

                // Apply the optimization to the document.
                xmlDoc.OptimizeResources(opt);

                // Save the optimized PDF.
                xmlDoc.Save(outputPdfPath);
            }
        }

        // Clean up temporary XML file.
        try { File.Delete(tempXmlPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Compressed PDF saved to '{outputPdfPath}'.");
    }
}