using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf; // Core Aspose.Pdf namespace provides PDF document handling and metadata access

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

        // Retrieve the current application version
        string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0";

        // Load the PDF document inside a using block to ensure proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Set the CreatorTool property in XMP metadata via the Metadata dictionary
            // The XMP key for CreatorTool is "xmp:CreatorTool"
            pdfDoc.Metadata["xmp:CreatorTool"] = $"MyApp {version}";

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"CreatorTool set to 'MyApp {version}' and saved to '{outputPath}'.");
    }
}
