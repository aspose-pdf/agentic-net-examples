using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input_with_xfa.pdf";
        const string xmlDataPath = "xfa_data.xml";
        const string outputPath = "output_filled.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load the XML containing XFA data
        XmlDocument xfaXml = new XmlDocument();
        xfaXml.Load(xmlDataPath);

        // Open the PDF document that contains an XFA form
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Ensure the document actually has an XFA form
            if (!pdfDoc.Form.HasXfa)
            {
                Console.Error.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Assign the XFA data to the form
            pdfDoc.Form.AssignXfa(xfaXml);

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"XFA data imported and saved to '{outputPath}'.");
    }
}