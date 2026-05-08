using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";      // PDF with form fields
        const string xmlDataPath  = "data.xml";       // Transformed XML containing field values
        const string outputPdfPath = "output.pdf";    // Resulting PDF with populated fields

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Load the transformed XML into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlDataPath);

                // Assign the XML data to the PDF form (XFA)
                // This populates the form fields with values from the XML
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Save the updated PDF
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