using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // PDF with form fields
        const string xmlDataPath   = "data.xml";       // Transformed XML containing field values
        const string outputPdfPath = "output.pdf";     // PDF after repopulating fields

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Load the transformed XML into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlDataPath);

                // Assign the XML (XFA) data to the PDF form
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