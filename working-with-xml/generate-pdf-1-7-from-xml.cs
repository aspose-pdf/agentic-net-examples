using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input XML and output PDF
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";
        const string logPath = "conversion_log.xml"; // log file for conversion

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML with default options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Use a using block for deterministic disposal of the Document
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Change PDF version to 1.7 using Document.Convert()
            doc.Convert(logPath, PdfFormat.v_1_7, ConvertErrorAction.Delete);

            // Save the generated PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with version 1.7 at '{pdfPath}'.");
    }
}
