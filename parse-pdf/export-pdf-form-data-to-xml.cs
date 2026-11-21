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
        const string inputPath = "input_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document that contains a form
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document actually has form fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Manually build an XML representation of the form data
            XDocument xmlDoc = new XDocument(
                new XElement("FormData",
                    doc.Form.Fields.Select(f =>
                        new XElement("Field",
                            new XAttribute("Name", f.Name ?? f.FullName ?? string.Empty),
                            new XAttribute("Value", f.Value?.ToString() ?? string.Empty)
                        )
                    )
                )
            );

            // Export the XML to a MemoryStream
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // Save the XDocument into the stream (UTF‑8 without BOM)
                using (StreamWriter writer = new StreamWriter(xmlStream, new System.Text.UTF8Encoding(false), 1024, true))
                {
                    xmlDoc.Save(writer);
                }

                // Reset the stream position to read the XML content
                xmlStream.Position = 0;
                using (StreamReader reader = new StreamReader(xmlStream))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("Exported Form XML:");
                    Console.WriteLine(xmlContent);
                }
            }
        }
    }
}
