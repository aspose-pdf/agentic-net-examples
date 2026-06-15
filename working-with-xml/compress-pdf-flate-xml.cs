using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string intermediateXmlPath = "temp.xml";
        const string outputPdfPath = "compressed.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the original PDF and export it to XML.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            pdfDoc.SaveXml(intermediateXmlPath);
        }

        // Load the PDF back from the XML representation.
        using (Document doc = new Document(intermediateXmlPath, new XmlLoadOptions()))
        {
            // Enable Flate compression for all PDF objects.
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };
            doc.OptimizeResources(opt);

            // Save the compressed PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPdfPath}'.");
    }
}