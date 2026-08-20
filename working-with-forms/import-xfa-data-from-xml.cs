using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

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

        // Load the PDF document (XFA form must be present)
        using (Document doc = new Document(pdfPath))
        {
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

        Console.WriteLine($"XFA data imported and saved to '{outputPath}'.");
    }
}