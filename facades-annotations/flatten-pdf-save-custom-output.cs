using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Facades namespace (required by task)

class PdfFlattener
{
    /// <summary>
    /// Flattens the specified PDF and saves the result into a custom output directory.
    /// </summary>
    /// <param name="inputPdfPath">Full path to the source PDF file.</param>
    /// <param name="outputDirectory">Directory where the flattened PDF will be stored.</param>
    /// <param name="outputFileName">File name for the flattened PDF (without path).</param>
    public static void FlattenAndSave(string inputPdfPath, string outputDirectory, string outputFileName)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Combine directory and file name to get the full destination path
        string destPath = Path.Combine(outputDirectory, outputFileName);

        // Load, flatten, and save the PDF using the recommended lifecycle pattern
        using (Document doc = new Document(inputPdfPath))
        {
            // Remove all form fields and replace them with their appearance values
            doc.Flatten();

            // Save the flattened document to the custom location
            doc.Save(destPath);
        }

        Console.WriteLine($"Flattened PDF saved to: {destPath}");
    }

    // Example usage
    static void Main()
    {
        string inputPdf = "sample.pdf";                 // source PDF
        string outputDir = @"C:\Processed\Flattened";   // custom output directory
        string outputFile = "sample_flattened.pdf";     // desired output file name

        FlattenAndSave(inputPdf, outputDir, outputFile);
    }
}