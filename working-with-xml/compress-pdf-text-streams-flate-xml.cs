using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath = "input.pdf";
        const string intermediateXmlPath = "temp.xml";
        const string outputPdfPath = "compressed.pdf";

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Load the original PDF and save it as XML.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // XmlSaveOptions are required for exporting to XML format.
            XmlSaveOptions xmlSaveOptions = new XmlSaveOptions();
            pdfDoc.Save(intermediateXmlPath, xmlSaveOptions);
        }

        // -----------------------------------------------------------------
        // Step 2: Load the XML back into a PDF document.
        // -----------------------------------------------------------------
        XmlLoadOptions xmlLoadOptions = new XmlLoadOptions();
        using (Document xmlDoc = new Document(intermediateXmlPath, xmlLoadOptions))
        {
            // -----------------------------------------------------------------
            // Step 3: Apply Flate compression to text streams via optimization.
            // -----------------------------------------------------------------
            OptimizationOptions optOptions = new OptimizationOptions
            {
                // Compress all PDF objects (including text streams) using Flate.
                CompressObjects = true
            };
            xmlDoc.OptimizeResources(optOptions);

            // -----------------------------------------------------------------
            // Step 4: Save the compressed PDF.
            // -----------------------------------------------------------------
            xmlDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPdfPath}'.");
    }
}