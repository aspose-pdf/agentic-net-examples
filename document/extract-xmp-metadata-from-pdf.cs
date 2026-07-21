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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create (or overwrite) the output XML file and write the XMP metadata into it
            using (FileStream xmlStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
            {
                pdfDoc.GetXmpMetadata(xmlStream);
            }
        }

        Console.WriteLine($"XMP metadata extracted to: {outputXmlPath}");
    }
}