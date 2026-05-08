using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfCreatorConfigurator
{
    // Default creator value used when none is supplied.
    private const string DefaultCreator = "Aspose.Pdf.Facades";

    /// <summary>
    /// Sets the Creator metadata of a PDF file.
    /// If <paramref name="creator"/> is null or empty, the default creator is applied.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the modified PDF will be saved.</param>
    /// <param name="creator">Optional creator string; if omitted the default is used.</param>
    public static void SetCreator(string inputPath, string outputPath, string creator = null)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Determine which creator value to apply.
            string creatorToSet = string.IsNullOrWhiteSpace(creator) ? DefaultCreator : creator;

            // Apply the creator value to the document metadata.
            // Document.Info is of type DocumentInfo; its Creator property stores the creator string.
            doc.Info.Creator = creatorToSet;

            // Save the updated PDF. No SaveOptions are needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with Creator=\"{(string.IsNullOrWhiteSpace(creator) ? DefaultCreator : creator)}\" to '{outputPath}'.");
    }

    // Example usage.
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Call without specifying a creator to use the default.
        SetCreator(inputPdf, outputPdf);

        // Call with a custom creator.
        SetCreator(inputPdf, "custom_output.pdf", "MyCustomTool");
    }
}