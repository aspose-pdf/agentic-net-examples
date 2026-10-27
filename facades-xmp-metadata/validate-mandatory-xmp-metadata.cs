using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class XmpValidator
{
    // List of mandatory XMP fields (full qualified names as they appear in the XMP XML)
    private static readonly string[] MandatoryFields = new[]
    {
        "dc:title",
        "dc:creator",
        "dc:description"
    };

    // Validates that all mandatory XMP fields exist in the PDF.
    // Returns true if validation succeeds, false otherwise.
    private static bool ValidateXmp(string pdfPath)
    {
        // Load XMP metadata using the Facades API.
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(pdfPath);                     // Load the PDF into the XMP handler
        byte[] xmpBytes = xmp.GetXmpMetadata();   // Retrieve XMP as a byte array

        if (xmpBytes == null || xmpBytes.Length == 0)
        {
            Console.Error.WriteLine("No XMP metadata found in the document.");
            return false;
        }

        // Convert the byte array to a UTF‑8 string.
        string xmpXml = Encoding.UTF8.GetString(xmpBytes);

        // Parse the XML for easy querying.
        XDocument doc;
        try
        {
            doc = XDocument.Parse(xmpXml);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse XMP XML: {ex.Message}");
            return false;
        }

        // Build a set of element names present in the XMP (ignoring namespace prefixes).
        HashSet<string> presentFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var element in doc.Descendants())
        {
            // Combine prefix (if any) with local name to match the format in MandatoryFields.
            string local = element.Name.LocalName;
            string nsUri = element.Name.NamespaceName;

            // Add the plain local name.
            presentFields.Add(local);

            // Add with common prefixes if the namespace matches known ones.
            if (nsUri.Contains("dc/"))
                presentFields.Add($"dc:{local}");
            else if (nsUri.Contains("pdf/"))
                presentFields.Add($"pdf:{local}");
            else if (nsUri.Contains("xmp/"))
                presentFields.Add($"xmp:{local}");
        }

        // Verify each mandatory field.
        bool allPresent = true;
        foreach (string field in MandatoryFields)
        {
            if (!presentFields.Contains(field))
            {
                Console.Error.WriteLine($"Missing mandatory XMP field: {field}");
                allPresent = false;
            }
        }

        return allPresent;
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "validated_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Perform XMP validation.
        if (!ValidateXmp(inputPdf))
        {
            Console.Error.WriteLine("PDF validation failed. Publication aborted.");
            return;
        }

        // Validation succeeded – copy the PDF to the publication location.
        // Use the core Document API inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF passed XMP validation and was saved to '{outputPdf}'.");
    }
}