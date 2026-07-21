using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to simple key‑value configuration file
        const string configPath = "config.txt";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Expected entries:
        // XslFoFile=path\to\input.xslfo
        // XslFile=path\to\transform.xsl   (optional)
        string xslFoFile = null;
        string xslFile   = null;

        foreach (var line in File.ReadAllLines(configPath))
        {
            if (line.StartsWith("XslFoFile=", StringComparison.OrdinalIgnoreCase))
                xslFoFile = line.Substring("XslFoFile=".Length).Trim();
            else if (line.StartsWith("XslFile=", StringComparison.OrdinalIgnoreCase))
                xslFile = line.Substring("XslFile=".Length).Trim();
        }

        if (string.IsNullOrWhiteSpace(xslFoFile) || !File.Exists(xslFoFile))
        {
            Console.Error.WriteLine("Valid XSL‑FO file path is required in the configuration.");
            return;
        }

        // Create load options – if an XSLT file is supplied, use the constructor that accepts it
        XslFoLoadOptions loadOptions = string.IsNullOrWhiteSpace(xslFile)
            ? new XslFoLoadOptions()
            : new XslFoLoadOptions(xslFile);

        // Load the XSL‑FO document (with optional XSLT) and convert to PDF
        using (Document pdfDoc = new Document(xslFoFile, loadOptions))
        {
            string outputPdf = Path.ChangeExtension(xslFoFile, ".pdf");
            pdfDoc.Save(outputPdf); // Save using the standard Document.Save method
            Console.WriteLine($"PDF generated successfully: {outputPdf}");
        }
    }
}