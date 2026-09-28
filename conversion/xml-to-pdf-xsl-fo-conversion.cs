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

        try
        {
            // Load the XML file using XSL‑FO load options
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Save the resulting document as PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"XML successfully converted to PDF: '{pdfPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}