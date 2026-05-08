using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "portfolio.pdf";          // PDF portfolio file
        const string outputDirectory = "ExtractedFiles";      // Destination folder

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        try
        {
            // Load the PDF document (core API, wrapped in using for proper disposal)
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Access the collection of embedded files
                Aspose.Pdf.EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

                // If there are no embedded files, inform the user
                if (embeddedFiles == null || embeddedFiles.Count == 0)
                {
                    Console.WriteLine("No embedded files found in the PDF portfolio.");
                    return;
                }

                // Iterate over the embedded files (Aspose collections are 1‑based)
                for (int i = 1; i <= embeddedFiles.Count; i++)
                {
                    // Each item is a FileSpecification representing an embedded file
                    Aspose.Pdf.FileSpecification fileSpec = embeddedFiles[i];

                    // Retrieve the original file name; fallback to a generated name if missing
                    string fileName = !string.IsNullOrEmpty(fileSpec.Name)
                        ? fileSpec.Name
                        : $"EmbeddedFile_{i}";

                    string outputPath = Path.Combine(outputDirectory, fileName);

                    // Save the embedded file's contents to disk
                    using (Stream contentStream = fileSpec.Contents)
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        contentStream.CopyTo(outStream);
                    }

                    Console.WriteLine($"Extracted: {fileName}");
                }
            }

            Console.WriteLine($"All embedded files have been saved to '{outputDirectory}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}