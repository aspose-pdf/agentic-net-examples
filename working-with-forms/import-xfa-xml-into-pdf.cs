using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string transformedXml = "data.xml";
        const string outputPdfPath  = "output.pdf";

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the transformed XML data into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            using (FileStream xmlStream = File.OpenRead(transformedXml))
            {
                xmlDoc.Load(xmlStream);
            }

            // Assign the XML data to the PDF form (XFA)
            // This imports field values from the XML into the PDF form fields
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Save the updated PDF (lifecycle rule: save inside using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF fields repopulated and saved to '{outputPdfPath}'.");
    }
}