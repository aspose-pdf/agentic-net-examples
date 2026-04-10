using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";   // PDF with AcroForm fields
        const string xmlDataPath     = "data.xml";      // XML source file
        const string outputPdfPath   = "filled.pdf";    // Resulting PDF

        // Verify input files exist
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

        // Load the XML document once – we'll query it with XPath
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlDataPath);
        var navigator = xmlDoc.CreateNavigator();

        // Open the PDF document inside a using block (ensures disposal)
        using (Document pdfDoc = new Document(pdfTemplatePath))
        {
            // Initialise the Form facade with the opened document
            using (Form form = new Form(pdfDoc))
            {
                // Map each PDF field name to an XPath expression that extracts its value
                var fieldMap = new Dictionary<string, string>
                {
                    { "FirstName", "/Root/Person/FirstName" },
                    { "LastName",  "/Root/Person/LastName" },
                    { "BirthDate", "/Root/Person/BirthDate" },
                    { "IsMember",  "/Root/Member/IsActive" }   // example boolean field
                };

                // Iterate over the mapping and fill the corresponding fields
                foreach (var kvp in fieldMap)
                {
                    string fieldName = kvp.Key;   // full field name in the PDF
                    string xpath     = kvp.Value; // XPath to locate the value in XML

                    // Retrieve the node; if it doesn't exist, skip this field
                    var node = navigator.SelectSingleNode(xpath);
                    if (node == null) continue;

                    // Try to interpret the node value as a boolean; otherwise treat as string
                    if (bool.TryParse(node.Value, out bool boolVal))
                    {
                        // Fill a checkbox/radio field with a boolean value
                        form.FillField(fieldName, boolVal);
                    }
                    else
                    {
                        // Fill a regular text field with the string value
                        form.FillField(fieldName, node.Value);
                    }
                }

                // Save the updated PDF to the desired output path
                form.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Filled PDF saved to '{outputPdfPath}'.");
    }
}