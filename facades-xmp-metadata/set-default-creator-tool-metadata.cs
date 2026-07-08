using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Default creator tool name used when none is supplied
    private const string DefaultCreatorTool = "MyDefaultCreatorTool";

    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Optional creator tool name – replace with actual value or leave null/empty
        string creatorTool = null; // e.g., could be obtained from args or config

        // Apply default if no creator tool is provided
        if (string.IsNullOrWhiteSpace(creatorTool))
        {
            creatorTool = DefaultCreatorTool;
        }

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, set the Creator metadata, and save the result
        using (Document pdfDoc = new Document(inputPath))
        {
            // Set the Creator information in the document metadata
            pdfDoc.Info.Creator = creatorTool;

            // Save the modified PDF to the output path
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with Creator set to '{creatorTool}'.");
    }
}