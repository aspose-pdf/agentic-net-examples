using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string inputXmlPath = "input.xml";   // XML representation of a PDF
        const string outputPdfPath = "compressed.pdf";

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputXmlPath}");
            return;
        }

        // Load the PDF from its XML representation.
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Ensure deterministic disposal of the Document.
        using (Document pdfDoc = new Document(inputXmlPath, loadOptions))
        {
            // Create optimization options that compress streams, images and remove unused data.
            OptimizationOptions opt = OptimizationOptions.All();
            opt.CompressObjects = true;
            opt.RemoveUnusedObjects = true;
            opt.RemoveUnusedStreams = true;
            opt.LinkDuplicateStreams = true;
            // Optional image compression settings.
            opt.CompressImages = true;
            opt.ImageQuality = 75; // adjust quality as needed (0‑100)

            // Apply the optimization to the document.
            pdfDoc.OptimizeResources(opt);

            // Save the optimized (compressed) PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{outputPdfPath}'.");
    }
}
