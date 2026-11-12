using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // List of mandatory XMP fields (XPath expressions relative to the XMP root)
    static readonly string[] RequiredXmpFields = new[]
    {
        "dc:title",          // Document title
        "dc:creator",        // Author/creator
        "dc:description",    // Description
        "xmp:CreateDate"     // Creation date
    };

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "validated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract XMP metadata using the Facades API (PdfXmpMetadata)
        // -----------------------------------------------------------------
        PdfXmpMetadata xmpFacade = new PdfXmpMetadata();
        string xmpXml;

        try
        {
            // GetXmpMetadata returns the raw XMP packet as a byte[]
            byte[] xmpBytes = xmpFacade.GetXmpMetadata(inputPath);
            // Convert the byte[] to a UTF‑8 string for XML parsing
            xmpXml = Encoding.UTF8.GetString(xmpBytes);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read XMP metadata: {ex.Message}");
            return;
        }

        if (string.IsNullOrWhiteSpace(xmpXml))
        {
            Console.Error.WriteLine("No XMP metadata found in the PDF.");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Parse the XMP XML and verify mandatory fields
        // -----------------------------------------------------------------
        XDocument xmpDoc;
        try
        {
            xmpDoc = XDocument.Parse(xmpXml);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Invalid XMP XML format: {ex.Message}");
            return;
        }

        // XMP uses namespaces; define them for XPath queries
        XNamespace dc = "http://purl.org/dc/elements/1.1/";
        XNamespace xmp = "http://ns.adobe.com/xap/1.0/";

        bool allFieldsPresent = true;

        foreach (string field in RequiredXmpFields)
        {
            // Resolve namespace prefix
            XName name = field switch
            {
                var f when f.StartsWith("dc:") => dc + f.Substring(3),
                var f when f.StartsWith("xmp:") => xmp + f.Substring(4),
                _ => XName.Get(field) // fallback (unlikely)
            };

            // Look for the element anywhere in the document
            XElement element = xmpDoc.Root?.Descendants(name).FirstOrDefault();

            if (element == null || string.IsNullOrWhiteSpace(element.Value))
            {
                Console.Error.WriteLine($"Missing mandatory XMP field: {field}");
                allFieldsPresent = false;
            }
        }

        if (!allFieldsPresent)
        {
            Console.Error.WriteLine("PDF validation failed. Publication aborted.");
            return;
        }

        // -----------------------------------------------------------------
        // 3. All required XMP fields are present – proceed to "publish"
        //    (here we simply copy the PDF to the output location)
        // -----------------------------------------------------------------
        try
        {
            // Use the core Document API inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF validated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF save: {ex.Message}");
        }
    }
}
