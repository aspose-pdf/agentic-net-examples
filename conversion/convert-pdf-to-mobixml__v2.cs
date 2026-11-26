using System;
using System.IO;
using Aspose.Pdf;               // Document, MobiXmlSaveOptions are in this namespace

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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create default save options for MobiXml format
            MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

            // Save the document as MobiXml using the explicit options
            doc.Save(outputPath, mobiOptions);
        }

        Console.WriteLine($"MobiXml file generated at '{outputPath}'.");
    }
}