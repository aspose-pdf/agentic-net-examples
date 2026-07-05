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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a file stream for the XML output
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                // Extract the embedded XMP metadata into the stream
                pdfDoc.GetXmpMetadata(xmlStream);
            }
        }

        Console.WriteLine($"XMP metadata saved to '{outputXml}'.");
    }
}