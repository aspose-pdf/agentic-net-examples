using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF portfolio
        const string pdfPath = "portfolio.pdf";
        // Directory where extracted files will be saved
        const string outputDir = "ExtractedFiles";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Verify that the PDF contains embedded files
                if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF.");
                    return;
                }

                // Iterate over each embedded file in the portfolio using FileSpecification
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // Determine the full path for the extracted file
                    string destPath = Path.Combine(outputDir, fileSpec.Name);

                    // Handle possible name collisions by appending a numeric suffix
                    int duplicateIndex = 1;
                    while (File.Exists(destPath))
                    {
                        string nameWithoutExt = Path.GetFileNameWithoutExtension(fileSpec.Name);
                        string ext = Path.GetExtension(fileSpec.Name);
                        destPath = Path.Combine(outputDir, $"{nameWithoutExt}_{duplicateIndex}{ext}");
                        duplicateIndex++;
                    }

                    // Write the embedded file's stream to disk
                    using (FileStream fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                    using (Stream embeddedStream = fileSpec.Contents)
                    {
                        embeddedStream.CopyTo(fileStream);
                    }

                    Console.WriteLine($"Extracted: {fileSpec.Name} → {destPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
