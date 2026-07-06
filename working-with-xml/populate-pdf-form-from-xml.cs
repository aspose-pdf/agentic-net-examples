using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields
        const string xmlDataPath     = "data.xml";       // XML containing field values
        const string outputPdfPath   = "filled_form.pdf";

        // Verify files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the PDF document (core API)
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                // Load the XML data into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlDataPath);

                // Assign the XML data to the XFA form (populates fields)
                // The Form property is always available; AssignXfa handles both XFA and standard forms.
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Save the populated PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}