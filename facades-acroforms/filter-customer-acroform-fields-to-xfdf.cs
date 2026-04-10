using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputXmlPath = "customer_fields.xfdf";

        // ------------------------------------------------------------
        // 1. Create a sample PDF in memory with a few form fields.
        //    Some field names start with "Customer" – these are the ones
        //    we want to keep after the export.
        // ------------------------------------------------------------
        using (var pdfStream = new MemoryStream())
        {
            // Build the PDF document.
            var doc = new Document();
            var page = doc.Pages.Add();

            // Add a text box field named "CustomerName".
            var customerName = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 300, 720))
            {
                PartialName = "CustomerName",
                Value = "John Doe"
            };
            doc.Form.Add(customerName);

            // Add another text box field named "CustomerEmail".
            var customerEmail = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 660, 300, 680))
            {
                PartialName = "CustomerEmail",
                Value = "john.doe@example.com"
            };
            doc.Form.Add(customerEmail);

            // Add a field that should be filtered out (does NOT start with "Customer").
            var orderId = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 620, 300, 640))
            {
                PartialName = "OrderId",
                Value = "12345"
            };
            doc.Form.Add(orderId);

            // Save the PDF to the memory stream.
            doc.Save(pdfStream);
            pdfStream.Position = 0; // Reset for reading.

            // ------------------------------------------------------------
            // 2. Use the Form facade to export XFDF (XML) to another memory stream.
            // ------------------------------------------------------------
            // Fully‑qualified Facades Form to avoid the Aspose.Pdf.Forms.Form ambiguity.
            using (var form = new Aspose.Pdf.Facades.Form())
            {
                form.BindPdf(pdfStream); // Bind the in‑memory PDF.

                using (var xfdfStream = new MemoryStream())
                {
                    form.ExportXfdf(xfdfStream);
                    xfdfStream.Position = 0; // Reset for reading.

                    // ------------------------------------------------
                    // 3. Load the XFDF XML and filter the <field> elements.
                    // ------------------------------------------------
                    var xfdfDoc = XDocument.Load(xfdfStream);
                    var fieldsElement = xfdfDoc.Root?.Element("fields");
                    if (fieldsElement != null)
                    {
                        var allowedFields = fieldsElement
                            .Elements("field")
                            .Where(f => (f.Attribute("name")?.Value?.StartsWith("Customer") ?? false))
                            .ToList();

                        fieldsElement.RemoveAll();
                        foreach (var field in allowedFields)
                        {
                            fieldsElement.Add(field);
                        }
                    }

                    // ------------------------------------------------
                    // 4. Persist the filtered XFDF to the output file.
                    // ------------------------------------------------
                    using (var outFile = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                    {
                        xfdfDoc.Save(outFile);
                    }
                }
            }
        }

        Console.WriteLine($"Filtered XFDF saved to '{outputXmlPath}'.");
    }
}
