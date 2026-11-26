using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the template PDF, the XML with placeholders, and the output PDF
        const string pdfTemplatePath = "template.pdf";
        const string xmlTemplatePath = "data_template.xml";
        const string outputPdfPath   = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlTemplatePath))
        {
            Console.Error.WriteLine($"XML template not found: {xmlTemplatePath}");
            return;
        }

        // Runtime values to replace placeholders in the XML
        var placeholderValues = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToString("yyyy-MM-dd") },
            // add more key/value pairs as needed
        };

        // Load the XML template as text
        string xmlContent = File.ReadAllText(xmlTemplatePath);

        // Replace each placeholder (e.g., {{Name}}) with its runtime value
        foreach (var kvp in placeholderValues)
        {
            string placeholder = $"{{{{{kvp.Key}}}}}"; // creates {{Name}} style token
            xmlContent = xmlContent.Replace(placeholder, kvp.Value);
        }

        // Load the PDF template, bind the XML data, and save the result
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Bind the XML data to the XFA form inside the PDF.
            // Document.BindXml expects the XML as a string, not an XmlDocument.
            pdfDoc.BindXml(xmlContent);

            // Save the bound PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF generated and saved to '{outputPdfPath}'.");
    }
}
