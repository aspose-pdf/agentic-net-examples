using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF with form fields
        const string xmlPath   = "data.xml";    // XML file containing form data
        const string outputPdf = "filled_output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Load the XML data into an XmlDocument
                XmlDocument xmlData = new XmlDocument();
                xmlData.Load(xmlPath);

                // If the PDF contains an XFA form, assign the XML data to it.
                // This updates matching fields automatically.
                if (pdfDoc.Form.HasXfa)
                {
                    pdfDoc.Form.AssignXfa(xmlData);
                }
                else
                {
                    Console.WriteLine("PDF does not contain an XFA form; no data imported.");
                }

                // Save the updated PDF (lifecycle: save)
                pdfDoc.Save(outputPdf);
                Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}