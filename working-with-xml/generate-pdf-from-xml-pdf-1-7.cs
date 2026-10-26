using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // Path to the source XML data
        const string outputPdf = "output.pdf"; // Desired PDF output path
        const string conversionLog = "conversion_log.xml"; // Log file for conversion process

        // Verify that the XML source file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML data into a PDF Document using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document doc = new Document(xmlPath, loadOptions))
        {
            // Convert the document to PDF version 1.7 for maximum compatibility
            doc.Convert(conversionLog, PdfFormat.v_1_7, ConvertErrorAction.Delete);

            // Save the document as a PDF file (default PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated with version 1.7: {outputPdf}");
    }
}
