using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "filtered_fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF form using the Facades Form class
        using (Form form = new Form(inputPdf))
        {
            // Export all form fields to a temporary memory stream as XML
            using (MemoryStream tempStream = new MemoryStream())
            {
                form.ExportXml(tempStream);
                tempStream.Position = 0; // Reset stream for reading

                // Load the exported XML into an XDocument
                XDocument xfdfDoc = XDocument.Load(tempStream);

                // The XFDF structure: <xfdf><fields><field name="...">...</field>...</fields></xfdf>
                XNamespace ns = xfdfDoc.Root.Name.Namespace;
                XElement fieldsElement = xfdfDoc.Root.Element(ns + "fields");

                if (fieldsElement != null)
                {
                    // Keep only fields whose name starts with "Customer"
                    XElement filteredFields = new XElement(ns + "fields",
                        fieldsElement.Elements(ns + "field")
                                     .Where(f => ((string)f.Attribute("name"))?.StartsWith("Customer") == true));

                    // Replace the original fields collection with the filtered one
                    fieldsElement.ReplaceWith(filteredFields);
                }

                // Save the filtered XML to the desired output file
                xfdfDoc.Save(outputXml);
            }
        }

        Console.WriteLine($"Filtered XML saved to '{outputXml}'.");
    }
}