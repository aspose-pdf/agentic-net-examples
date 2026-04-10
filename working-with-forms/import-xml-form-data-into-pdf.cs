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

        // XML data to import into the form fields (as a string)
        string xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<FormData>
    <Field name=""FirstName"">John</Field>
    <Field name=""LastName"">Doe</Field>
    <Field name=""Email"">john.doe@example.com</Field>
</FormData>";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Parse the XML string into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlData)))
            {
                xmlDoc.Load(xmlStream);
            }

            // Assign the XFA data (XML) to the form.
            // This imports the XML data into the PDF form fields.
            pdfDoc.Form.AssignXfa(xmlDoc);

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}