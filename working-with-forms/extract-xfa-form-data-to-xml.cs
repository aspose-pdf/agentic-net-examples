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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the PDF contains an XFA form
                if (!doc.Form.HasXfa)
                {
                    Console.WriteLine("The PDF does not contain an XFA form.");
                    return;
                }

                // Access the XFA object
                XFA xfa = doc.Form.XFA;

                // Retrieve the XDP (XML Data Package) as an XmlDocument
                XmlDocument xdp = xfa.XDP;

                // Save the XML data to a file
                xdp.Save(outputXml);
                Console.WriteLine($"XFA data extracted and saved to '{outputXml}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}