using System;
using System.IO;
using Aspose.Pdf;
using System.Xml;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "data.xml";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the XFA XML data
        XmlDocument xfaData = new XmlDocument();
        xfaData.Load(xmlPath);

        // Load the PDF, assign XFA data, and save
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Verify that the PDF contains an XFA form
            if (!pdfDoc.Form.HasXfa)
            {
                Console.Error.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Assign the new XFA data to the form
            pdfDoc.Form.AssignXfa(xfaData);

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"XFA data imported and saved to '{outputPath}'.");
    }
}