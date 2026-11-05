using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.mobi";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF and convert it to MobiXml using default save options
        using (Document doc = new Document(inputPath))
        {
            // MobiXmlSaveOptions has sensible defaults; no custom settings required
            MobiXmlSaveOptions mobiOpts = new MobiXmlSaveOptions();

            // When saving to a non‑PDF format, the SaveOptions instance must be supplied
            doc.Save(outputPath, mobiOpts);
        }

        Console.WriteLine($"PDF successfully converted to MobiXml: {outputPath}");
    }
}