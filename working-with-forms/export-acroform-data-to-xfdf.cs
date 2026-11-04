using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing AcroForm fields
        const string inputPdfPath = @"C:\Docs\input.pdf";

        // UNC path to the network share where the XML will be saved
        const string outputXmlPath = @"\\fileserver\share\AcroFormData.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Build an XML representation of the AcroForm fields using the core API
                XDocument xmlDoc = new XDocument(new XElement("AcroFormData"));

                if (pdfDocument.Form != null && pdfDocument.Form.Fields != null)
                {
                    foreach (Field field in pdfDocument.Form.Fields)
                    {
                        // Field name – use Name if available, otherwise FullName
                        string fieldName = field.Name ?? field.FullName ?? "UnnamedField";

                        // Field value – most field types expose a Value property that can be converted to string
                        string fieldValue = field.Value?.ToString() ?? string.Empty;

                        // Create an element for the field
                        XElement fieldElement = new XElement("Field",
                            new XAttribute("Name", fieldName),
                            new XAttribute("Type", field.GetType().Name),
                            new XAttribute("Value", fieldValue)
                        );

                        xmlDoc.Root.Add(fieldElement);
                    }
                }

                // Ensure the directory for the UNC path exists (optional, but safe)
                string directory = Path.GetDirectoryName(outputXmlPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Save the XML to the network share
                using (FileStream fs = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                {
                    xmlDoc.Save(fs);
                }
            }

            Console.WriteLine($"AcroForm XML successfully saved to: {outputXmlPath}");
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., I/O issues, permission problems)
            Console.Error.WriteLine($"Error extracting AcroForm XML: {ex.Message}");
        }
    }
}
