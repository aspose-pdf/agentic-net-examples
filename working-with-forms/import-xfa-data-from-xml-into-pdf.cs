using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";
        const string xmlPath = "data.xml";
        const string outputPath = "filled.pdf";

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

        // Load the PDF that contains an XFA form
        using (Document doc = new Document(pdfPath))
        {
            // Verify that the PDF actually has an XFA form
            if (!doc.Form.HasXfa)
            {
                Console.Error.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Assign the XFA data to the form
            doc.Form.AssignXfa(xfaData);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"XFA data imported successfully. Output saved to '{outputPath}'.");
    }
}