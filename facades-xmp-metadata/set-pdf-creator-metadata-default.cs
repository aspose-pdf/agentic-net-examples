using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Default creator tool name used when no creator is supplied.
    private const string DefaultCreatorTool = "MyDefaultCreatorTool";

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Example: caller does not provide a creator value.
        SetCreatorTool(inputPdf, outputPdf, creator: null);
    }

    /// <summary>
    /// Sets the PDF Creator metadata using Aspose.Pdf.Document.
    /// If <paramref name="creator"/> is null or empty, a default value is applied.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF file.</param>
    /// <param name="outputPath">Path where the modified PDF will be saved.</param>
    /// <param name="creator">Optional creator string; if omitted, a default is used.</param>
    private static void SetCreatorTool(string inputPath, string outputPath, string? creator)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDoc = new Document(inputPath);

        // Determine the creator value to use.
        string creatorToSet = string.IsNullOrWhiteSpace(creator) ? DefaultCreatorTool : creator;

        // Set the Creator metadata.
        pdfDoc.Info.Creator = creatorToSet;

        // Save the modified PDF to the desired location.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Creator set to \"{creatorToSet}\" and saved to \"{outputPath}\".");
    }
}
