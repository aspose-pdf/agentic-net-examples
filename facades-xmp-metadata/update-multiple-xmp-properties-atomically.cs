using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(inputPath);
            }
        }

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // Register the XMP namespace that will be used for the properties.
        // The RegisterNamespaceUri method adds a prefix‑URI mapping for XMP metadata.
        pdf.Metadata.RegisterNamespaceUri("xmp", "http://ns.adobe.com/xap/1.0/");

        // Add / update multiple XMP properties in a single transaction.
        // DateTime values are converted to ISO‑8601 strings because XMP expects string literals.
        pdf.Metadata["xmp:CreatorTool"] = "MyApplication";
        pdf.Metadata["xmp:CreateDate"]   = DateTime.UtcNow.ToString("o");
        pdf.Metadata["xmp:ModifyDate"]   = DateTime.UtcNow.ToString("o");
        pdf.Metadata["xmp:Nickname"]    = "SampleDoc";

        // Persist the changes atomically.
        pdf.Save(outputPath);

        Console.WriteLine($"XMP properties updated and saved to '{outputPath}'.");
    }
}
