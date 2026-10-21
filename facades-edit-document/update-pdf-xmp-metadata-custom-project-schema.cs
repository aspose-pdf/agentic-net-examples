using System;
using System.IO;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define a prefix for the custom schema (the actual namespace URI is not required for the dictionary API)
            const string prefix = "proj";

            // Add custom metadata entries using the document's Metadata dictionary
            doc.Metadata[$"{prefix}:ProjectID"] = "MyProject123";
            doc.Metadata[$"{prefix}:Version"]   = "1.0.0";

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated metadata saved to '{outputPath}'.");
    }
}
