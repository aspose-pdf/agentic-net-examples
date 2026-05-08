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
        const string conversionLog = "conversion_log.xml"; // log file required by Convert()

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML with default options
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Use a using block to ensure the Document is disposed properly
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Change PDF version to 1.7 – Document.Version is read‑only, so use Convert()
            doc.Convert(conversionLog, PdfFormat.v_1_7, ConvertErrorAction.Delete);

            // Save the generated PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with version 1.7: {pdfPath}");
    }
}
