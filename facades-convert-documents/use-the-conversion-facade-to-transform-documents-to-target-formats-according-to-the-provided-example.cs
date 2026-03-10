using System;
using System.IO;
using Aspose.Pdf;   // All save option classes (HtmlSaveOptions, DocSaveOptions, etc.) are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPdf = "input.pdf";

        // Desired output files in different formats
        const string outputHtml = "output.html";
        const string outputDocx = "output.docx";
        const string outputXml  = "output.xml";

        // Perform conversions
        ConvertToFormat(inputPdf, outputHtml);
        ConvertToFormat(inputPdf, outputDocx);
        ConvertToFormat(inputPdf, outputXml);
    }

    /// <summary>
    /// Converts a source document to the format implied by the destination file extension.
    /// Uses the static Document.Convert method (the conversion facade) when a SaveOptions instance is required.
    /// </summary>
    static void ConvertToFormat(string srcPath, string dstPath)
    {
        if (!File.Exists(srcPath))
        {
            Console.Error.WriteLine($"Source file not found: {srcPath}");
            return;
        }

        // Determine target format from file extension
        string ext = Path.GetExtension(dstPath).ToLowerInvariant();

        // Choose the appropriate SaveOptions subclass for the target format
        SaveOptions saveOptions = ext switch
        {
            ".html" => new HtmlSaveOptions(),
            ".doc"  => new DocSaveOptions { Format = DocSaveOptions.DocFormat.Doc },
            ".docx" => new DocSaveOptions { Format = DocSaveOptions.DocFormat.DocX },
            ".xml"  => new XmlSaveOptions(),
            ".svg"  => new SvgSaveOptions(),
            ".xps"  => new XpsSaveOptions(),
            // For PDF output we can save directly without SaveOptions
            ".pdf"  => null,
            _       => null
        };

        try
        {
            if (ext == ".pdf")
            {
                // Direct PDF save – load the document and call Save
                using (Document doc = new Document(srcPath))
                {
                    doc.Save(dstPath);
                }
            }
            else if (saveOptions != null)
            {
                // Use the conversion facade: static Document.Convert method
                // Source is a PDF, so LoadOptions can be null
                Document.Convert(srcPath, null, dstPath, saveOptions);
            }
            else
            {
                Console.Error.WriteLine($"Unsupported target format: {dstPath}");
                return;
            }

            Console.WriteLine($"Successfully converted '{srcPath}' to '{dstPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed for '{dstPath}': {ex.Message}");
        }
    }
}