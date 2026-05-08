using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // XML data to be imported into the form (as a string)
        string xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<FormData>
    <Field1>Value 1</Field1>
    <Field2>Value 2</Field2>
</FormData>";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Parse the XML string into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlData)))
            {
                xmlDoc.Load(xmlStream);
            }

            // Import the XML data into the form.
            // For XFA forms, AssignXfa replaces the XFA data packet with the supplied XML.
            // This method is part of the core Aspose.Pdf API (no Facades namespace required).
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}