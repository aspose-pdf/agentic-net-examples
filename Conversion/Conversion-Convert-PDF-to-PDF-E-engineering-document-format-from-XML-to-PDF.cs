using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains all required types

class Program
{
    static void Main()
    {
        // Directory that contains the source XML file
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input XML (PDFXML) file path
        string xmlFile = Path.Combine(dataDir, "input.xml");

        // Output PDF file path (PDF/E engineering document – using PDF_X_3 as the closest supported format)
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // Log file for conversion messages
        string logFile = Path.Combine(dataDir, "conversion.log");

        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML source not found: {xmlFile}");
            return;
        }

        // Load the XML document with default XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Ensure the Document is disposed properly
        using (Document doc = new Document(xmlFile, loadOptions))
        {
            // Convert the loaded document to the target PDF/E format.
            // Aspose.Pdf does not expose a PDF/E enum in this version,
            // so PDF_X_3 is used as the closest supported engineering format.
            doc.Convert(logFile, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted PDF
            doc.Save(pdfFile);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{pdfFile}'.");
    }
}