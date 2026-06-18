using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // PDF containing XFA form
        const string outputXmlPath = "xfa_data.xml"; // Destination XML file

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the form object
            Form pdfForm = pdfDoc.Form;

            // Verify that the document actually contains an XFA form
            if (!pdfForm.HasXfa)
            {
                Console.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Retrieve the XFA data package (XDP) as an XmlDocument
            XmlDocument xfaXml = pdfForm.XFA.XDP;

            // Save the XML to the specified file
            xfaXml.Save(outputXmlPath);

            Console.WriteLine($"XFA data extracted and saved to '{outputXmlPath}'.");
        }
    }
}