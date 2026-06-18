using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF portfolio file
        const string inputPdf = "portfolio.pdf";

        // Output root folder where extracted files will be placed
        const string outputRoot = "ExtractedFiles";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output root directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF document (portfolio) inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // The EmbeddedFiles collection holds all files attached to the portfolio
            var embeddedFiles = doc.EmbeddedFiles;

            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF portfolio.");
                return;
            }

            // Iterate over each embedded file specification
            foreach (FileSpecification fileSpec in embeddedFiles)
            {
                // The Name property may contain a path (e.g., "folder/subfolder/file.txt")
                // Use it to recreate the original folder hierarchy under the output root.
                string relativePath = fileSpec.Name ?? "UnnamedFile";

                // Combine with the output root and normalize directory separators
                string destinationPath = Path.Combine(outputRoot, relativePath);
                string destinationDir = Path.GetDirectoryName(destinationPath);

                // Create any necessary subdirectories
                if (!string.IsNullOrEmpty(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }

                // Write the file contents to disk
                using (FileStream outputStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    // The Contents stream contains the binary data of the embedded file
                    fileSpec.Contents.CopyTo(outputStream);
                }

                Console.WriteLine($"Extracted: {destinationPath}");
            }
        }

        Console.WriteLine("All embedded files have been extracted.");
    }
}