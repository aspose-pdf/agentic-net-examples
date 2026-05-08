using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "selected_fields.xml";

        // Names of the form fields you want to export
        string[] fieldsToExport = { "FirstName", "LastName", "Email" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Build a collection of the selected fields from the document's Form.Fields collection
            var selectedFieldElements = doc.Form?.Fields
                ?.OfType<Field>()
                ?.Where(f => fieldsToExport.Contains(f.PartialName))
                ?.Select(f =>
                {
                    // The value can be null for empty fields – treat it as an empty string
                    string value = f?.Value?.ToString() ?? string.Empty;
                    // Create <field name="..."><value>...</value></field>
                    return new XElement("field",
                        new XAttribute("name", f.PartialName),
                        new XElement("value", value)
                    );
                })
                ?.ToList();

            if (selectedFieldElements == null || selectedFieldElements.Count == 0)
            {
                Console.Error.WriteLine("No matching form fields were found in the PDF.");
                return;
            }

            // Build a new XFDF‑like document containing only the selected fields
            XDocument filteredXfdf = new XDocument(
                new XElement("xfdf",
                    new XElement("fields", selectedFieldElements)
                )
            );

            // Save the filtered XML to the requested output file
            filteredXfdf.Save(outputXml);
        }

        Console.WriteLine($"Selected form fields exported to '{outputXml}'.");
    }
}
