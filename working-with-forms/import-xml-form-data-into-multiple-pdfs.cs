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
        // Paths to the PDFs that need to share the same form data
        string[] pdfInputs = { "DocumentA.pdf", "DocumentB.pdf", "DocumentC.pdf" };
        // Path to the XML file containing the form field values
        const string xmlDataPath = "FormData.xml";

        // Verify the XML file exists before processing
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load the XML once – it will be used for every PDF
        XDocument xmlDoc = XDocument.Load(xmlDataPath);
        // Expected simple structure: <form><field name="FieldName">Value</field>...</form>
        var xmlFields = xmlDoc.Descendants("field")
                              .Select(f => new
                              {
                                  Name = (string)f.Attribute("name"),
                                  Value = (string)f.Value
                              })
                              .Where(f => !string.IsNullOrEmpty(f.Name))
                              .ToList();

        // Process each PDF independently but using the same XML data
        foreach (string inputPath in pdfInputs)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"PDF file not found: {inputPath}");
                continue; // skip missing files
            }

            // Use a using block for deterministic disposal (rule: document-disposal-with-using)
            using (Document pdfDoc = new Document(inputPath))
            {
                // Ensure automatic recalculation of JavaScript‑based fields after we set values
                pdfDoc.Form.AutoRecalculate = true;

                // Apply each XML field value to the matching PDF form field
                foreach (var xmlField in xmlFields)
                {
                    var pdfField = pdfDoc.Form?.Fields?.FirstOrDefault(f => f.Name == xmlField.Name);
                    if (pdfField != null)
                    {
                        pdfField.Value = xmlField.Value;
                    }
                }

                // Determine an output file name (e.g., add "_Synced" suffix)
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(inputPath) + "_Synced.pdf");

                // Save the updated PDF (saving as PDF does not require SaveOptions)
                pdfDoc.Save(outputPath);

                Console.WriteLine($"Synchronized PDF saved: {outputPath}");
            }
        }
    }
}
