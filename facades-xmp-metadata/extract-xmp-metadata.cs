using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Dictionary<string, string> metadata = ExtractXmpMetadata(inputPath);

        Console.WriteLine("XMP Metadata:");
        foreach (KeyValuePair<string, string> entry in metadata)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    private static Dictionary<string, string> ExtractXmpMetadata(string pdfPath)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        using (Document doc = new Document(pdfPath))
        {
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            foreach (string key in xmp.Keys)
            {
                XmpValue value = xmp[key];
                string stringValue = value.ToStringValue();
                result.Add(key, stringValue);
            }
        }

        return result;
    }
}