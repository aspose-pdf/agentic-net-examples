using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormG.pdf";
        const string xfdfPath = "FormG_fields.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Enumerate all form fields and output their names and values
            foreach (Field field in doc.Form?.Fields ?? Array.Empty<Field>())
            {
                string name = field.FullName ?? field.Name ?? "<unnamed>";
                string value = field.Value?.ToString() ?? string.Empty;
                Console.WriteLine($"Field Name: {name}, Value: {value}");
            }

            // Build a minimal XFDF document containing the field data
            var xfdf = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("xfdf",
                    new XAttribute(XNamespace.Xmlns + "xfdf", "http://ns.adobe.com/xfdf/"),
                    new XElement("fields",
                        from Field f in doc.Form?.Fields ?? Array.Empty<Field>()
                        let name = f.FullName ?? f.Name ?? string.Empty
                        let value = f.Value?.ToString() ?? string.Empty
                        select new XElement("field",
                            new XAttribute("name", name),
                            new XElement("value", value)
                        )
                    )
                )
            );

            // Save the XFDF file
            xfdf.Save(xfdfPath);
        }

        Console.WriteLine($"Form fields exported to '{xfdfPath}'.");
    }
}
