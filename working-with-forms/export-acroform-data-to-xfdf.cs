using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Local PDF file to process
        const string pdfPath = @"C:\Docs\input.pdf";

        // UNC network share where the XML will be saved
        const string xmlNetworkPath = @"\\fileserver\shared\AcroFormData.xml";

        // Validate input PDF existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the target directory on the network share exists
        try
        {
            string? networkDir = Path.GetDirectoryName(xmlNetworkPath);
            if (string.IsNullOrEmpty(networkDir))
                throw new InvalidOperationException("Invalid network path.");

            if (!Directory.Exists(networkDir))
                Directory.CreateDirectory(networkDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to prepare network directory: {ex.Message}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Check whether the PDF contains an AcroForm
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the PDF.");
                return;
            }

            // Build an XML representation of the form fields
            XDocument xmlDoc = new XDocument(new XElement("AcroForm"));
            XElement root = xmlDoc.Root!;

            foreach (Field field in doc.Form.Fields)
            {
                string name = field.Name ?? field.FullName ?? string.Empty;
                string value = field.Value?.ToString() ?? string.Empty;
                XElement fieldElement = new XElement("Field",
                    new XAttribute("name", name),
                    new XAttribute("value", value));
                root.Add(fieldElement);
            }

            // Save the XML to the network share
            xmlDoc.Save(xmlNetworkPath);
        }

        Console.WriteLine($"AcroForm XML exported to: {xmlNetworkPath}");
    }
}
