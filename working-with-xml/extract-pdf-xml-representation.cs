using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Path to the source PDF
        const string outputXml = "output.xml";  // Path where the XML representation will be saved

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Save the internal XML representation of the PDF
            pdfDoc.SaveXml(outputXml);
        }

        Console.WriteLine($"XML representation saved to '{outputXml}'.");
    }
}