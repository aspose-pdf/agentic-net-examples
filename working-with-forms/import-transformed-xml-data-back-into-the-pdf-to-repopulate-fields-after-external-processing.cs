using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // Original PDF with form fields
        const string xmlPath   = "transformed.xml"; // XML containing updated field values
        const string outputPath = "output.pdf";   // PDF after importing XML data

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Load the transformed XML into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            // Import the XML data into the PDF form (XFA)
            // Form.AssignXfa replaces the XFA data with the provided XML
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Save the updated PDF (lifecycle: save)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported XML saved to '{outputPath}'.");
    }
}