using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.mobi";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document within a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create default MobiXmlSaveOptions (all SaveOptions are in Aspose.Pdf namespace).
            MobiXmlSaveOptions mobiOpts = new MobiXmlSaveOptions();

            // Save the document as MobiXml using the options.
            doc.Save(outputPath, mobiOpts);
        }

        Console.WriteLine($"PDF successfully converted to MobiXml: {outputPath}");
    }
}