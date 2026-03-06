using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the XFA template PDF, the XML data file, and the output PDF.
        const string templatePath = "template.pdf";
        const string xmlDataPath  = "data.xml";
        const string outputPath   = "filled.pdf";

        // Verify that the required files exist.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the XFA form template.
            using (Document doc = new Document(templatePath))
            {
                // Load the XML document containing the data to populate the form.
                XmlDocument xmlData = new XmlDocument();
                xmlData.Load(xmlDataPath);

                // Assign the XML data to the XFA form.
                doc.Form.AssignXfa(xmlData);

                // Flatten the form so that field values become part of the page content.
                doc.Form.Flatten();

                // Save the resulting PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"XFA form populated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}