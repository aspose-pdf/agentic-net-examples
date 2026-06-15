using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // optional, kept for completeness

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";
        const string conversionLog = "conversion_log.xml"; // log file for conversion details

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML without any XSL transformation
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Create PDF document from XML
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Change PDF version to 1.7 using Document.Convert (Version property is read‑only)
            pdfDoc.Convert(conversionLog, PdfFormat.v_1_7, ConvertErrorAction.Delete);

            // Save the generated PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with version 1.7: {pdfPath}");
    }
}
