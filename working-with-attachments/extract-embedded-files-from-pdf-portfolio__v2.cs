using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF Portfolio file
        const string portfolioPath = "portfolio.pdf";

        // Root folder where extracted files will be written
        const string outputRoot = "ExtractedFiles";

        if (!File.Exists(portfolioPath))
        {
            Console.Error.WriteLine($"File not found: {portfolioPath}");
            return;
        }

        // Ensure the output root directory exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the PDF Portfolio inside a using block for deterministic disposal
            using (Document doc = new Document(portfolioPath))
            {
                // Check if the document contains any embedded files
                if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF portfolio.");
                    return;
                }

                // Iterate over each embedded file using FileSpecification (the correct API)
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // Preserve any folder hierarchy encoded in the file name
                    string targetPath = Path.Combine(outputRoot, fileSpec.Name);

                    // Ensure the directory for the target file exists
                    string targetDir = Path.GetDirectoryName(targetPath);
                    if (!string.IsNullOrEmpty(targetDir))
                    {
                        Directory.CreateDirectory(targetDir);
                    }

                    // Write the embedded file's content to disk
                    using (FileStream outStream = File.Create(targetPath))
                    using (Stream content = fileSpec.Contents)
                    {
                        content.CopyTo(outStream);
                    }

                    Console.WriteLine($"Extracted: {targetPath}");
                }
            }

            Console.WriteLine("All embedded files have been extracted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
