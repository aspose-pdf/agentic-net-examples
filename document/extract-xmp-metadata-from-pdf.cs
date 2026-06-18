using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "metadata.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a file stream to write the XMP metadata (XML format)
            using (FileStream outStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                // Extract the embedded XMP metadata into the stream
                pdfDoc.GetXmpMetadata(outStream);
            }
        }

        Console.WriteLine($"XMP metadata extracted to '{outputXml}'.");
    }
}