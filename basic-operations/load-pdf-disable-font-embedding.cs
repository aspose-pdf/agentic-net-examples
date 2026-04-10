using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf; // LoadOptions and SaveOptions are in this namespace

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

        // Create a Document instance and load the PDF with custom LoadOptions.
        // HtmlLoadOptions is used here because it provides the IsEmbedFonts property,
        // which disables font embedding during the load process.
        using (Document doc = new Document())
        {
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            loadOptions.IsEmbedFonts = false; // disable embedding of fonts

            // Load the PDF file using the custom options.
            doc.LoadFrom(inputPath, loadOptions);

            // Save the document using PdfSaveOptions (default behavior).
            PdfSaveOptions saveOptions = new PdfSaveOptions();
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}