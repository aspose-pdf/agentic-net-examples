using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Get the current application version (e.g., 1.0.0.0)
        string appVersion = Assembly.GetExecutingAssembly()
                                    .GetName()
                                    .Version
                                    ?.ToString() ?? "unknown";

        // Open the PDF, set the XMP CreatorTool property, and save
        using (Document doc = new Document(inputPath))
        {
            // XMP metadata is stored in the Metadata dictionary.
            // The standard key for CreatorTool is "xmp:CreatorTool".
            doc.Metadata["xmp:CreatorTool"] = appVersion;

            doc.Save(outputPath);
        }

        Console.WriteLine($"CreatorTool set to '{appVersion}' and saved to '{outputPath}'.");
    }
}