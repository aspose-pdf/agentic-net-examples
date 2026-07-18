using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields (XFA)
        const string xmlDataPath     = "data.xml";       // XML containing data records
        const string outputPdfPath   = "filled.pdf";

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

        // Load the PDF document (form)
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Load the XML data
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlDataPath);

            // Assign the XML data to the XFA form (if present)
            if (pdfDoc.Form.HasXfa)
            {
                pdfDoc.Form.AssignXfa(xmlDoc);
            }
            else
            {
                Console.Error.WriteLine("The PDF does not contain an XFA form.");
                return;
            }

            // Save the filled PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }
}