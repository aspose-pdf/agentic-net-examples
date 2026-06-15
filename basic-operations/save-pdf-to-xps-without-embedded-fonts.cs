using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xps";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (no special load options are required for this operation)
        using (Document doc = new Document(inputPath))
        {
            // Configure save options to disable embedding of TrueType fonts
            XpsSaveOptions saveOptions = new XpsSaveOptions
            {
                UseEmbeddedTrueTypeFonts = false // fonts will not be embedded in the output XPS
            };

            // Save the document using the configured options
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"Document saved to '{outputPath}' with fonts not embedded.");
    }
}