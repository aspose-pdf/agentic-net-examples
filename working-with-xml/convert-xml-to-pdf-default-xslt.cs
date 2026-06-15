using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load options for XML without a custom XSLT (default transformation)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Convert XML to PDF using the load options
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created at '{pdfPath}'.");
    }
}