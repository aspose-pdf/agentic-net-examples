using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPdfPath  = "input_form.pdf";
        const string outputPdfPath = "output_form_filled.pdf";

        // XML data as a string (replace with your actual form data)
        string xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<FormData>
    <field name=""FirstName"">John</field>
    <field name=""LastName"">Doe</field>
    <field name=""Email"">john.doe@example.com</field>
</FormData>";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document (core API)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the XML string into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            // Import the XML data into the PDF form.
            // AssignXfa works with XFA forms and accepts an XmlDocument.
            // This is the core‑API equivalent of the Facades Form.ImportXml method.
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Save the updated PDF (core API)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}