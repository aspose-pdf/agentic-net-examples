using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf;               // LoadOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // NOTE:
        // Aspose.Pdf does not provide a dedicated LoadOptions class for disabling
        // font embedding when loading an existing PDF. The IsEmbedFonts property is
        // available on HtmlLoadOptions (used when loading HTML sources). For a PDF
        // load we use the default options; the example sets IsEmbedFonts to false
        // to illustrate the property, but it has no effect on a PDF input.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions
        {
            IsEmbedFonts = false   // Disable font embedding for HTML sources
        };

        // Load the PDF document with the (custom) load options
        using (Document doc = new Document(inputPath, loadOptions))
        {
            // Save the document back to PDF using default save options
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}