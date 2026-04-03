using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input_with_xfa.pdf";
        const string xfaXmlPath    = "xfa_data.xml";
        const string outputPdfPath = "output_filled.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfaXmlPath))
        {
            Console.Error.WriteLine($"XFA XML not found: {xfaXmlPath}");
            return;
        }

        // Load the XFA XML data
        XmlDocument xfaData = new XmlDocument();
        xfaData.Load(xfaXmlPath);

        // Open the PDF containing an XFA form, assign the new XFA data, and save
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document actually contains an XFA form
            if (!pdfDoc.Form.HasXfa)
            {
                Console.Error.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Assign the XFA data to the form
            pdfDoc.Form.AssignXfa(xfaData);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"XFA data imported and saved to '{outputPdfPath}'.");
    }
}