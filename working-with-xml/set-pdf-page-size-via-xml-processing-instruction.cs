using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlInputPath  = "input.xml";
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"File not found: {xmlInputPath}");
            return;
        }

        // Load the XML file into a PDF document using XmlLoadOptions.
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlInputPath, loadOptions))
        {
            // Default page size (A4) – will be overridden if a processing instruction is found.
            double pageWidth  = 595; // points
            double pageHeight = 842; // points

            // Scan the XML for a processing instruction that specifies page size.
            // Expected format: <?pdf-page-size width="595" height="842"?>
            using (FileStream fs = File.OpenRead(xmlInputPath))
            using (XmlReader reader = XmlReader.Create(fs))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.ProcessingInstruction &&
                        string.Equals(reader.Name, "pdf-page-size", StringComparison.OrdinalIgnoreCase))
                    {
                        // For a processing instruction, XmlReader.Value contains the raw data after the target name.
                        // Example:  width="595" height="842"
                        string data = reader.Value;
                        foreach (string part in data.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            string[] kv = part.Split('=');
                            if (kv.Length != 2) continue;

                            string key   = kv[0].Trim().ToLowerInvariant();
                            string value = kv[1].Trim().Trim('"');

                            if (key == "width"  && double.TryParse(value, out double w)) pageWidth  = w;
                            if (key == "height" && double.TryParse(value, out double h)) pageHeight = h;
                        }
                    }
                }
            }

            // Apply the determined page size to all pages in the document.
            foreach (Page page in pdfDoc.Pages)
            {
                page.SetPageSize(pageWidth, pageHeight);
            }

            // Save the resulting PDF.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF generated at '{pdfOutputPath}'.");
    }
}
