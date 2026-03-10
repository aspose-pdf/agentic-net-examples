using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf.Facades;

class UpdatePdfMetadata
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string configXmlPath  = "metadata.xml";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(configXmlPath))
        {
            Console.Error.WriteLine($"Configuration XML not found: {configXmlPath}");
            return;
        }

        // Load XML configuration
        XDocument configDoc = XDocument.Load(configXmlPath);
        XElement root = configDoc.Root;
        if (root == null)
        {
            Console.Error.WriteLine("Invalid configuration file – missing root element.");
            return;
        }

        // Extract standard metadata elements (optional)
        string title        = (string)root.Element("Title")        ?? string.Empty;
        string author       = (string)root.Element("Author")       ?? string.Empty;
        string subject      = (string)root.Element("Subject")      ?? string.Empty;
        string keywords     = (string)root.Element("Keywords")     ?? string.Empty;
        string creator      = (string)root.Element("Creator")      ?? string.Empty;

        // Dates – expect ISO 8601 format; fallback to current date if parsing fails
        DateTime creationDate = DateTime.Now;
        DateTime.TryParse((string)root.Element("CreationDate"), out creationDate);
        DateTime modDate = DateTime.Now;
        DateTime.TryParse((string)root.Element("ModDate"), out modDate);

        // Use PdfFileInfo facade to modify metadata
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the source PDF
            pdfInfo.BindPdf(inputPdfPath);

            // Set standard properties
            if (!string.IsNullOrEmpty(title))   pdfInfo.Title   = title;
            if (!string.IsNullOrEmpty(author))  pdfInfo.Author  = author;
            if (!string.IsNullOrEmpty(subject)) pdfInfo.Subject = subject;
            if (!string.IsNullOrEmpty(keywords))pdfInfo.Keywords= keywords;
            if (!string.IsNullOrEmpty(creator)) pdfInfo.Creator = creator;

            // PdfFileInfo expects dates as strings in PDF date format ("yyyyMMddHHmmss")
            pdfInfo.CreationDate = creationDate.ToString("yyyyMMddHHmmss");
            pdfInfo.ModDate      = modDate.ToString("yyyyMMddHHmmss");

            // Apply custom metadata (if any)
            XElement customMeta = root.Element("CustomMeta");
            if (customMeta != null)
            {
                foreach (XElement meta in customMeta.Elements("Meta"))
                {
                    string name  = (string)meta.Element("Name");
                    string value = (string)meta.Element("Value");
                    if (!string.IsNullOrEmpty(name) && value != null)
                    {
                        pdfInfo.SetMetaInfo(name, value);
                    }
                }
            }

            // Save the updated PDF to a new file
            pdfInfo.SaveNewInfo(outputPdfPath);
        }

        Console.WriteLine($"Metadata updated successfully. Output saved to '{outputPdfPath}'.");
    }
}
