using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "data.xml";
        const string xslFoPath = "template.xslfo";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Use the XmlLoadOptions constructor that accepts a stream instead of assigning to the read‑only XslStream property.
        using (FileStream xslStream = File.OpenRead(xslFoPath))
        {
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslStream);

            // Load the XML document and apply the XSL‑FO transformation.
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                pdfDocument.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}
