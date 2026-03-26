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

        using (Document doc = new Document(inputPath))
        {
            // Set standard metadata
            doc.Info.Author = "John Doe";
            // Use Producer as a placeholder for publisher metadata
            doc.Info.Producer = "Acme Publishing";

            // If custom metadata is required, you can add it like this:
            // doc.Info.SetCustomMetadata("Publisher", "Acme Publishing");

            MobiXmlSaveOptions saveOptions = new MobiXmlSaveOptions();
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF converted to MobiXml: {outputPath}");
    }
}