using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the XMP packet, and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string xmpPacketPath = "metadata.xmp";
        const string outputPdfPath = "output.pdf";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmpPacketPath))
        {
            Console.Error.WriteLine($"XMP metadata file not found: {xmpPacketPath}");
            return;
        }

        // Load the XMP metadata packet as a string (XML)
        string xmpXml = File.ReadAllText(xmpPacketPath);

        // Load the PDF document (this also loads existing metadata such as Title, Author, etc.)
        Document pdfDoc = new Document(inputPdfPath);

        // ---------------------------------------------------------------------
        // Merge the XMP packet with the existing PDF metadata.
        // Aspose.Pdf does not expose a direct SetXmpMetadata method. Instead, XMP
        // metadata is manipulated through the Document.Metadata indexer. The
        // example below demonstrates how to register a custom namespace and copy
        // a few common XMP properties from the raw packet. For a full‑packet
        // merge you would parse the XML and set each required property via the
        // indexer.
        // ---------------------------------------------------------------------
        // Register a custom namespace (replace with the one used in your XMP)
        const string customPrefix = "custom";
        const string customNamespace = "http://example.com/custom";
        pdfDoc.Metadata.RegisterNamespaceUri(customPrefix, customNamespace);

        // Very simple parsing – extract <dc:title> and <dc:creator> as examples.
        // In production code use a proper XML parser (e.g., System.Xml.Linq).
        string title = ExtractTagValue(xmpXml, "dc:title");
        string creator = ExtractTagValue(xmpXml, "dc:creator");

        if (!string.IsNullOrEmpty(title))
            pdfDoc.Metadata["xmp:dc:Title"] = title;
        if (!string.IsNullOrEmpty(creator))
            pdfDoc.Metadata["xmp:dc:Creator"] = creator;

        // Save the PDF with the merged metadata to a new file.
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"Merged metadata saved to '{outputPdfPath}'.");
    }

    // Minimal XML tag extractor – not robust, only for demo purposes.
    private static string ExtractTagValue(string xml, string tagName)
    {
        string openTag = $"<{tagName}>";
        string closeTag = $"</{tagName}>";
        int start = xml.IndexOf(openTag, StringComparison.OrdinalIgnoreCase);
        int end = xml.IndexOf(closeTag, StringComparison.OrdinalIgnoreCase);
        if (start >= 0 && end > start)
        {
            start += openTag.Length;
            return xml.Substring(start, end - start).Trim();
        }
        return string.Empty;
    }
}
