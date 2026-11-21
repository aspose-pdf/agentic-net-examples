using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // Required by task specification (even if not used directly)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cleared.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            ClearAllXmpMetadata(inputPath, outputPath);
            Console.WriteLine($"XMP metadata cleared. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Loads a PDF, removes all entries from the XMP metadata dictionary,
    /// and saves the result. The required PDF schema header is retained automatically
    /// by the Aspose.Pdf engine; we only clear user‑defined metadata entries.
    /// </summary>
    /// <param name="sourcePath">Path to the source PDF.</param>
    /// <param name="destPath">Path where the cleaned PDF will be saved.</param>
    static void ClearAllXmpMetadata(string sourcePath, string destPath)
    {
        // Load the document inside a using block for deterministic disposal.
        using (Document doc = new Document(sourcePath))
        {
            // The XMP metadata is exposed via the Metadata dictionary.
            // Clear all custom entries; the library will keep the mandatory PDF schema header.
            doc.Metadata.Clear();

            // Save the modified document.
            doc.Save(destPath);
        }
    }
}