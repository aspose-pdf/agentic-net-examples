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

        // Open the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a file stream to write the XMP metadata as XML
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                // Extract XMP metadata into the stream
                pdfDoc.GetXmpMetadata(xmlStream);
            }
        }

        Console.WriteLine($"XMP metadata saved to '{outputXml}'.");
    }
}