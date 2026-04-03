using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXml = "selected_fields.xml";

        // List of field names that should be exported
        var fieldsToExport = new List<string> { "FirstName", "LastName", "Email" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the Facades Form object
        using (var form = new Form())
        {
            form.BindPdf(inputPdf);

            // Export all form data to a temporary XML stream
            using (var allDataStream = new MemoryStream())
            {
                form.ExportXml(allDataStream);
                allDataStream.Position = 0; // reset for reading

                // Load the exported XML
                XDocument xmlDoc = XDocument.Load(allDataStream);

                // The XML structure produced by ExportXml looks like:
                // <xfdf>
                //   <fields>
                //     <field name="FieldName"><value>...</value></field>
                //   </fields>
                // </xfdf>
                // Filter only the requested fields
                var filteredFields = xmlDoc
                    .Descendants("field")
                    .Where(f => fieldsToExport.Contains((string)f.Attribute("name")))
                    .ToList();

                // Remove all existing fields and add back only the filtered ones
                var fieldsParent = xmlDoc.Descendants("fields").FirstOrDefault();
                if (fieldsParent != null)
                {
                    fieldsParent.RemoveAll(); // clear existing
                    foreach (var field in filteredFields)
                    {
                        fieldsParent.Add(field);
                    }
                }

                // Save the filtered XML to the target file
                xmlDoc.Save(outputXml);
                Console.WriteLine($"Selected fields exported to '{outputXml}'.");
            }
        }
    }
}