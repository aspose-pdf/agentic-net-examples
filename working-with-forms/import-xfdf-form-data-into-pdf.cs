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
        const string pdfPath = "input.pdf";
        const string xmlDataPath = "data.xml";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // -----------------------------------------------------------------
                // Import form data from the XML file.
                // The Aspose.Pdf.Forms.Form class does not expose an ImportXml method
                // in the core API (it exists only in the Facades wrapper, which we are
                // prohibited from using). Therefore we manually parse the XML and set
                // matching field values.
                // -----------------------------------------------------------------
                XDocument xmlDoc = XDocument.Load(xmlDataPath);
                var root = xmlDoc.Root;
                if (root != null && doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Try to find an element whose name matches the field name.
                        // XML element names are case‑sensitive; adjust as needed.
                        var element = root.Element(field.Name);
                        if (element != null)
                        {
                            field.Value = element.Value;
                        }
                    }
                }

                // Save the updated PDF. No SaveOptions needed for PDF output.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
