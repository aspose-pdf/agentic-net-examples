using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input file path and output PDF path
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: Program <inputFile> <outputPdf>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Determine appropriate load options based on file extension
            LoadOptions loadOptions = GetLoadOptions(Path.GetExtension(inputPath));

            // Load the source document (with or without load options)
            Document pdfDocument = loadOptions != null
                ? new Document(inputPath, loadOptions)
                : new Document(inputPath);

            // Save the resulting PDF document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Conversion successful. PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }

    // Returns a suitable LoadOptions instance for the given file extension,
    // or null if the format can be loaded directly without special options.
    private static LoadOptions GetLoadOptions(string extension)
    {
        if (string.IsNullOrEmpty(extension))
            return null;

        switch (extension.ToLowerInvariant())
        {
            case ".html":
                return new HtmlLoadOptions();
            case ".epub":
                return new EpubLoadOptions();
            case ".cgm":
                return new CgmLoadOptions();
            case ".xml":
                return new XmlLoadOptions();
            case ".xslfo":
                return new XslFoLoadOptions();
            case ".pcl":
                return new PclLoadOptions();
            case ".xps":
                return new XpsLoadOptions();
            case ".svg":
                return new SvgLoadOptions();
            case ".mht":
                return new MhtLoadOptions();
            case ".ps":
                return new PsLoadOptions();
            case ".md":
                return new MdLoadOptions();
            case ".txt":
                return new TxtLoadOptions();
            case ".aps":
                return new ApsLoadOptions();
            case ".pdfxml":
                return new PdfXmlLoadOptions();
            case ".ofd":
                return new OfdLoadOptions();
            case ".djvu":
                return new DjvuLoadOptions();
            case ".cdr":
                return new CdrLoadOptions();
            default:
                // No special load options required for other formats (e.g., PDF)
                return null;
        }
    }
}