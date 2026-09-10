using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Default creator tool name used when no creator is supplied.
    private const string DefaultCreatorTool = "MyDefaultCreatorTool";

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Optional creator value; set to null or empty to trigger default.
        string creator = null; // or set to "CustomCreator"

        try
        {
            SetCreatorIfMissing(inputPdf, outputPdf, creator);
            Console.WriteLine($"PDF saved with creator info to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Copies a PDF file and ensures the Creator metadata is set.
    /// If the source PDF already has a Creator value, it is preserved.
    /// If the Creator is missing or empty, the provided creator (or a default) is applied.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the resulting PDF will be saved.</param>
    /// <param name="creator">Optional creator string; if null or empty, a default is used.</param>
    private static void SetCreatorIfMissing(string inputPath, string outputPath, string creator)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input file not found: {inputPath}");

        // PdfFileInfo works with the Facades API to edit document metadata.
        PdfFileInfo fileInfo = new PdfFileInfo();

        // Bind the existing PDF file.
        fileInfo.BindPdf(inputPath);

        // Determine the creator to apply.
        string currentCreator = fileInfo.Creator;
        if (string.IsNullOrWhiteSpace(currentCreator))
        {
            // Use the supplied creator if provided; otherwise fall back to the default.
            fileInfo.Creator = string.IsNullOrWhiteSpace(creator) ? DefaultCreatorTool : creator;
        }

        // Save the updated PDF to the output location.
        fileInfo.Save(outputPath);
    }
}