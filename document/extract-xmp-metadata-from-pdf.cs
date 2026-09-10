using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXmlPath = "metadata.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a file stream to receive the XMP metadata
            using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
            {
                // Extract XMP metadata from the PDF into the stream
                pdfDoc.GetXmpMetadata(xmlStream);
            }
        }

        Console.WriteLine($"XMP metadata extracted to '{outputXmlPath}'.");
    }
}