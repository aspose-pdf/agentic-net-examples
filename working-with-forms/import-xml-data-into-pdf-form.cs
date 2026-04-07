using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // For Form.AssignXfa

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // Original PDF with form fields
        const string transformedXml = "data.xml";       // XML containing updated field values
        const string outputPdfPath  = "output.pdf";     // PDF after repopulating fields

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(transformedXml))
        {
            Console.Error.WriteLine($"XML not found: {transformedXml}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create/load)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Load the transformed XML into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(transformedXml);

                // Assign the XML (XFA) data to the PDF form
                // This populates the form fields with values from the XML
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Save the updated PDF (lifecycle: save)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF fields repopulated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}