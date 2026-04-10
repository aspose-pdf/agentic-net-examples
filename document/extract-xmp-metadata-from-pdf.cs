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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Create or overwrite the XML file and write the XMP metadata into it
                using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                {
                    pdfDoc.GetXmpMetadata(xmlStream);
                }
            }

            Console.WriteLine($"XMP metadata extracted to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting XMP metadata: {ex.Message}");
        }
    }
}