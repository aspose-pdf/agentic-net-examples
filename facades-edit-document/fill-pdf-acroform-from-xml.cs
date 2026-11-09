using System;
using System.IO;
using System.Linq;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfInputPath = "template.pdf";
        const string xmlDataPath = "data.xml";
        const string pdfOutputPath = "filled.pdf";

        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load XML once
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlDataPath);

            // Load PDF document
            using (Document pdfDoc = new Document(pdfInputPath))
            // Create Form facade bound to the document
            using (Form pdfForm = new Form(pdfDoc))
            {
                // Retrieve all AcroForm field names via the document's Form.Fields collection
                var fieldNames = pdfDoc.Form?.Fields?.Select(f => f.Name).ToArray() ?? Array.Empty<string>();

                foreach (string fieldName in fieldNames)
                {
                    // Simple XPath: element name matches field name
                    string xpath = $"//{fieldName}";
                    XmlNode node = xmlDoc.SelectSingleNode(xpath);

                    if (node != null && !string.IsNullOrEmpty(node.InnerText))
                    {
                        // Fill the field using the Form facade
                        pdfForm.FillField(fieldName, node.InnerText);
                    }
                }

                // Save the filled PDF
                pdfForm.Save(pdfOutputPath);
            }

            Console.WriteLine($"Form fields populated and saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
