using System;
using System.IO;
using Aspose.Pdf.Facades; // Facades API for metadata manipulation

// Configuration class to hold the default Creator value
public static class PdfConfiguration
{
    // Set this to the desired default Creator string
    public static string DefaultCreator { get; set; } = "MyApp PDF Generator";
}

// Helper class that updates the Creator metadata using the Facades API
public static class PdfCreatorHelper
{
    /// <summary>
    /// Sets the Creator metadata of a PDF file.
    /// If <paramref name="creator"/> is null or empty, the default value from <see cref="PdfConfiguration.DefaultCreator"/> is used.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file to modify.</param>
    /// <param name="creator">Optional Creator value; when omitted the default is applied.</param>
    public static void SetCreator(string pdfPath, string? creator = null)
    {
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Determine the value to write
        string valueToSet = string.IsNullOrWhiteSpace(creator)
            ? PdfConfiguration.DefaultCreator
            : creator;

        // Use PdfFileInfo (Facades) to modify document info without loading the full Document
        PdfFileInfo info = new PdfFileInfo(pdfPath);
        info.Creator = valueToSet;   // Set the Creator metadata (CreatorTool does not exist)
        info.Save(pdfPath);          // Persist changes back to the same file
    }
}

// Example usage
class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        // Ensure the file exists for the demo
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Option 1: Use an explicit Creator value
        PdfCreatorHelper.SetCreator(inputPdf, "CustomTool v1.2");

        // Option 2: Omit the value to apply the configured default
        PdfCreatorHelper.SetCreator(inputPdf);
        
        Console.WriteLine("Creator metadata updated successfully.");
    }
}