using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF containing the form
        const string pdfPath = "form.pdf";

        // Desired local XML file that will receive the exported data
        string xmlOutputPath = Path.Combine(Environment.CurrentDirectory, "formData.xml");

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Prepare the form fields to be filled
        NameValueCollection formData = new NameValueCollection
        {
            { "Name", "John Doe" },
            { "Email", "john.doe@example.com" },
            { "Comments", "Sample comment for offline collection." }
        };

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Fill the PDF form fields with the values from formData
                if (doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Use the field's Name (or FullName) to match the collection key
                        string fieldName = field.Name ?? field.FullName;
                        if (fieldName != null && formData[fieldName] != null)
                        {
                            field.Value = formData[fieldName];
                        }
                    }
                }

                // -----------------------------------------------------------------
                // Export the filled form data to an XML file.
                // The core Form API does not expose ExportXml – that functionality lives
                // in the Facade API, which is prohibited by the hard‑rule.  Instead we
                // manually serialize the field collection to a simple XML structure.
                // -----------------------------------------------------------------
                var root = new XElement("FormData");
                foreach (Field field in doc.Form?.Fields ?? Array.Empty<Field>())
                {
                    string name = field.Name ?? field.FullName ?? "UnnamedField";
                    string value = field.Value?.ToString() ?? string.Empty;
                    root.Add(new XElement("Field",
                                         new XAttribute("Name", name),
                                         new XAttribute("Value", value)));
                }

                var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
                xDoc.Save(xmlOutputPath);
            }

            Console.WriteLine($"Form data successfully exported to: {xmlOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during form processing: {ex.Message}");
        }
    }
}
