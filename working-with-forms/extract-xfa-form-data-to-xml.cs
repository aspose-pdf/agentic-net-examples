using System;
using System.IO;
using Aspose.Pdf;
using System.Xml;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing XFA form
        const string outputXml = "xfa_data.xml";   // Destination XML file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Verify that the document actually contains an XFA form
            if (!pdfDoc.Form.HasXfa)
            {
                Console.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Access the XFA data package (XDP) which is an XmlDocument
            XmlDocument xfaXml = pdfDoc.Form.XFA.XDP;

            // Save the XML representation to the specified file
            xfaXml.Save(outputXml);
        }

        Console.WriteLine($"XFA data extracted and saved to '{outputXml}'.");
    }
}