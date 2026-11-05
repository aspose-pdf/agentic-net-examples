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
        const string pdfPath = "template.pdf";   // Input PDF with form fields
        const string xmlPath = "data.xml";       // Transformed XML containing field values
        const string outputPath = "filled.pdf";  // Resulting PDF after import

        // Verify that required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the XML once and extract field name/value pairs
        XDocument xmlDoc = XDocument.Load(xmlPath);
        var xmlFields = xmlDoc.Descendants("field")
            .Select(f => new
            {
                Name = (string)f.Attribute("name"),
                Value = (string)f.Value
            })
            .Where(f => !string.IsNullOrEmpty(f.Name));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Enable automatic recalculation for JavaScript‑based fields (optional but recommended)
            pdfDoc.Form.AutoRecalculate = true;

            // Assign values from the XML to matching PDF form fields
            foreach (var xmlField in xmlFields)
            {
                var pdfField = pdfDoc.Form?.Fields?.FirstOrDefault(f => f.Name == xmlField.Name);
                if (pdfField != null)
                {
                    pdfField.Value = xmlField.Value;
                }
            }

            // Save the updated PDF. No SaveOptions needed because we are saving as PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported data saved to '{outputPath}'.");
    }
}
