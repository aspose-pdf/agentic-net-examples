using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "filtered_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Retrieve all form fields via the Document's Form collection
        var fields = pdfDocument.Form?.Fields;
        if (fields == null)
        {
            Console.WriteLine("No form fields found in the PDF.");
            return;
        }

        // Prepare XML writer
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            Encoding = Encoding.UTF8
        };

        using (XmlWriter writer = XmlWriter.Create(outputXml, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("Fields"); // root element

            // Write only fields whose names start with "Customer"
            foreach (Field field in fields)
            {
                string name = field.Name ?? field.FullName;
                if (!string.IsNullOrEmpty(name) &&
                    name.StartsWith("Customer", StringComparison.Ordinal))
                {
                    writer.WriteStartElement("Field");
                    writer.WriteAttributeString("Name", name);
                    writer.WriteString(field.Value?.ToString() ?? string.Empty);
                    writer.WriteEndElement(); // Field
                }
            }

            writer.WriteEndElement(); // Fields
            writer.WriteEndDocument();
        }

        Console.WriteLine($"Filtered form fields saved to '{outputXml}'.");
    }
}
