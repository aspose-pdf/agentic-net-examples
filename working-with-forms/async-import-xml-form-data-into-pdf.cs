using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Asynchronous entry point (C# 7.1+)
    static async Task Main(string[] args)
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "template.pdf";
        const string xmlDataPath   = "formData.xml";
        const string outputPdfPath = "filled.pdf";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Perform the import and save without blocking the main thread
            await ImportXmlFormDataAsync(inputPdfPath, xmlDataPath, outputPdfPath);
            Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Asynchronously loads a PDF, imports XML form data, and saves the result.
    private static async Task ImportXmlFormDataAsync(string pdfPath, string xmlPath, string outputPath)
    {
        // Load the XML once – this is CPU‑bound, so run it on a background thread.
        var xmlFields = await Task.Run(() =>
        {
            XDocument doc = XDocument.Load(xmlPath);
            // Expect XML like <fields><field name="FieldName">Value</field>...</fields>
            return doc.Descendants("field")
                      .Select(f => new
                      {
                          Name  = (string)f.Attribute("name"),
                          Value = (string)f.Value
                      })
                      .Where(f => !string.IsNullOrEmpty(f.Name))
                      .ToList();
        });

        // Work with the PDF document.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Enable automatic recalculation for JavaScript‑based fields (optional but common).
            pdfDoc.Form.AutoRecalculate = true;

            // Assign values to matching form fields – also CPU‑bound, so offload.
            await Task.Run(() =>
            {
                foreach (var xmlField in xmlFields)
                {
                    var pdfField = pdfDoc.Form?.Fields?.FirstOrDefault(f => f.Name == xmlField.Name);
                    if (pdfField != null)
                    {
                        pdfField.Value = xmlField.Value;
                    }
                }
            });

            // Save the modified PDF on a background thread to avoid blocking.
            await Task.Run(() => pdfDoc.Save(outputPath));
        }
    }
}
