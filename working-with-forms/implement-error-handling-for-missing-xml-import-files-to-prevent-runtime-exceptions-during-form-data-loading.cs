using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // PDF with a form
        const string xmlPath   = "formData.xml";   // XML file containing form data
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – '{pdfPath}'.");
            return;
        }

        // Verify that the XML file exists before attempting to load it
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML import file not found – '{xmlPath}'.");
            // Continue without importing XML data; the PDF will be saved unchanged
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // If the XML file is present, load it and assign to the XFA form
                if (File.Exists(xmlPath))
                {
                    // Load the XML into an XmlDocument (requires Aspose.Pdf.Forms namespace)
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlPath);

                    // Assign the XFA data to the form (ImportDataAction is for FDF; for XML use AssignXfa)
                    pdfDoc.Form.AssignXfa(xmlDoc);
                }

                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"PDF saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., malformed XML) and report them
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}