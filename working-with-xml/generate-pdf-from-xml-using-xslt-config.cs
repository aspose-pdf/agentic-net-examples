using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the simple key=value configuration file
        const string configPath = "config.txt";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load configuration entries
        var config = LoadConfig(configPath);

        // Required configuration keys
        if (!config.TryGetValue("XmlPath", out string xmlPath) ||
            !config.TryGetValue("XsltPath", out string xsltPath) ||
            !config.TryGetValue("PdfOutput", out string pdfOutput))
        {
            Console.Error.WriteLine("Missing required entries in config: XmlPath, XsltPath, PdfOutput");
            return;
        }

        // Verify that source files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        try
        {
            // Transform the XML using the XSLT into an intermediate HTML stream
            using (var htmlStream = TransformXmlToHtml(xmlPath, xsltPath))
            {
                // Load the generated HTML into an Aspose.Pdf Document
                var htmlLoadOptions = new HtmlLoadOptions();
                using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(htmlStream, htmlLoadOptions))
                {
                    // Save the result as PDF (extension is ignored, always PDF)
                    pdfDoc.Save(pdfOutput);
                }
            }

            Console.WriteLine($"PDF generated successfully: {pdfOutput}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF generation: {ex.Message}");
        }
    }

    // Transforms the XML file with the provided XSLT and returns a MemoryStream containing HTML
    private static MemoryStream TransformXmlToHtml(string xmlPath, string xsltPath)
    {
        var xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);

        var memoryStream = new MemoryStream();
        // Use the XSLT's output settings to ensure correct encoding
        var settings = xslt.OutputSettings ?? new XmlWriterSettings { Encoding = System.Text.Encoding.UTF8 };
        using (var writer = XmlWriter.Create(memoryStream, settings))
        {
            xslt.Transform(xmlPath, writer);
        }
        memoryStream.Position = 0; // Reset stream position for reading
        return memoryStream;
    }

    // Parses a simple configuration file where each line is "key=value"
    // Lines starting with '#' are treated as comments and ignored
    static Dictionary<string, string> LoadConfig(string path)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(path))
        {
            string trimmed = line.Trim();
            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                continue;

            int separatorIndex = trimmed.IndexOf('=');
            if (separatorIndex <= 0)
                continue; // skip malformed lines

            string key = trimmed.Substring(0, separatorIndex).Trim();
            string value = trimmed.Substring(separatorIndex + 1).Trim();
            dict[key] = value;
        }
        return dict;
    }
}
