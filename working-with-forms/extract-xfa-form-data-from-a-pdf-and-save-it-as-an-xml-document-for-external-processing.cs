using System;
using System.IO;
using Aspose.Pdf;
using System.Xml;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "xfa_data.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the PDF contains an XFA form
            if (!doc.Form.HasXfa)
            {
                Console.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Retrieve the XFA data package (XDP) as an XmlDocument
            XmlDocument xfaData = doc.Form.XFA.XDP;

            // Save the extracted XML to the specified file
            xfaData.Save(outputXml);
        }

        Console.WriteLine($"XFA form data has been saved to '{outputXml}'.");
    }
}