using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "template.pdf";      // PDF with form fields (XFA)
        const string transformedXml = "data.xml";          // XML containing new field values
        const string outputPdfPath  = "filled.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(transformedXml))
        {
            Console.Error.WriteLine($"XML data file not found: {transformedXml}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Load the transformed XML into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(transformedXml);

                // Assign the XML data to the PDF's XFA form (import field values)
                // Form.AssignXfa expects an XmlDocument
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Save the updated PDF (lifecycle: save)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF with imported XML data saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}