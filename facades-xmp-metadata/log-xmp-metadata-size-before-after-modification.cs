using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Log size of the XMP block before any changes
            long beforeSize = GetXmpBlockSize(doc);
            Console.WriteLine($"XMP block size before modification: {beforeSize} bytes");

            // Example modification: update a standard XMP entry
            // The XMP metadata is stored in the Metadata dictionary.
            // The dictionary expects an XmpValue, so we wrap the string.
            doc.Metadata["xmp:CreatorTool"] = new XmpValue("Aspose.Pdf.Modifier");

            // Log size of the XMP block after the modification
            long afterSize = GetXmpBlockSize(doc);
            Console.WriteLine($"XMP block size after modification: {afterSize} bytes");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }

    // Helper method to calculate the approximate size of the XMP block in bytes
    static long GetXmpBlockSize(Aspose.Pdf.Document document)
    {
        // If the XMP XML is stored under the key "xmp", use it directly
        if (document.Metadata.ContainsKey("xmp"))
        {
            // Metadata values are of type XmpValue; convert to string explicitly
            XmpValue xmpValue = document.Metadata["xmp"];
            string xmpXml = xmpValue?.ToString() ?? string.Empty;
            return Encoding.UTF8.GetByteCount(xmpXml);
        }

        // Fallback: sum the byte counts of all metadata values
        long total = 0;
        foreach (KeyValuePair<string, XmpValue> kvp in document.Metadata)
        {
            string val = kvp.Value?.ToString() ?? string.Empty;
            total += Encoding.UTF8.GetByteCount(val);
        }
        return total;
    }
}
