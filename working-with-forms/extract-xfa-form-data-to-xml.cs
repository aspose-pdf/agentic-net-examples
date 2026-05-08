using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the PDF contains an XFA form
            if (doc.Form.HasXfa)
            {
                // Retrieve the XFA data package as an XmlDocument
                XmlDocument xfaXml = doc.Form.XFA.XDP;

                // Save the XML representation to a file
                xfaXml.Save(outputXml);
                Console.WriteLine($"XFA data saved to '{outputXml}'.");
            }
            else
            {
                Console.WriteLine("The PDF does not contain an XFA form.");
            }
        }
    }
}