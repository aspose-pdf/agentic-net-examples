using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with form fields
        const string outputXml = "filtered_fields.xfdf"; // destination XML (XFDF)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document and bind it to a Form facade
        using (Document pdfDoc = new Document(inputPdf))
        using (Form form = new Form(pdfDoc))
        {
            // Export all form fields to a memory stream in XFDF (XML) format
            using (MemoryStream exportStream = new MemoryStream())
            {
                form.ExportXfdf(exportStream);
                exportStream.Position = 0; // rewind for reading

                // Load the exported XFDF into an XDocument for manipulation
                XDocument xfdfDoc = XDocument.Load(exportStream);

                // XFDF structure: <xfdf><fields><field name="...">...</field>...</fields></xfdf>
                // Keep only fields whose name starts with "Customer"
                var fieldsElement = xfdfDoc.Root?.Element("fields");
                if (fieldsElement != null)
                {
                    var filteredFields = fieldsElement.Elements("field")
                        .Where(f => ((string)f.Attribute("name"))?.StartsWith("Customer") == true)
                        .ToList();

                    // Remove all existing field elements and add back the filtered ones
                    fieldsElement.RemoveAll();
                    foreach (var field in filteredFields)
                        fieldsElement.Add(field);
                }

                // Save the filtered XFDF to the output file
                using (FileStream fileOut = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
                {
                    xfdfDoc.Save(fileOut);
                }
            }
        }

        Console.WriteLine($"Filtered XFDF saved to '{outputXml}'.");
    }
}