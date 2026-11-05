using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and output PPTX paths
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF, set document properties, then save as PPTX
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ----- Set built‑in document properties -----
            pdfDoc.Info.Subject = "Converted Presentation"; // built‑in property

            // ----- Add custom properties (key/value) -----
            // The DocumentInfo class does not expose a "Company" property; use the indexer to store custom metadata.
            pdfDoc.Info["Company"] = "Acme Corp";
            // Example of another custom entry
            pdfDoc.Info["Project"] = "Demo";

            // ----- Convert to PPTX -----
            // Save options classes are located directly in the Aspose.Pdf namespace (no Aspose.Pdf.Saving namespace).
            var pptxOptions = new PptxSaveOptions();

            // Save the document as PPTX. The Save method with SaveOptions ensures the correct format.
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}