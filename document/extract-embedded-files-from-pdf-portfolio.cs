using System;
using System.IO;
using Aspose.Pdf;

class PortfolioExtractor
{
    /// <summary>
    /// Extracts all embedded files from a PDF portfolio and saves them to the specified directory.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <param name="outputDirectory">Directory where extracted files will be saved.</param>
    public static void ExtractEmbeddedFiles(string pdfPath, string outputDirectory)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document (wrapped in using for proper disposal).
        using (Document doc = new Document(pdfPath))
        {
            // The EmbeddedFiles property gives access to the collection of embedded files.
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

            // If there are no embedded files, inform the user.
            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                Console.WriteLine("No embedded files found in the PDF.");
                return;
            }

            // Iterate over each embedded file and save it.
            foreach (object embeddedObj in embeddedFiles)
            {
                // Use dynamic to avoid compile‑time dependency on the EmbeddedFile type.
                dynamic embeddedFile = embeddedObj;

                // The Name property holds the original file name.
                string fileName = embeddedFile.Name as string;
                if (string.IsNullOrEmpty(fileName))
                {
                    // Fallback to a generated name if the original is missing.
                    fileName = Guid.NewGuid().ToString();
                }

                // Build the full path for the extracted file.
                string outputPath = Path.Combine(outputDirectory, fileName);

                // Save the embedded file to disk.
                embeddedFile.Save(outputPath);

                Console.WriteLine($"Extracted: {fileName} -> {outputPath}");
            }
        }
    }

    // Example usage.
    static void Main()
    {
        const string pdfPath = "portfolio.pdf";          // Input PDF portfolio
        const string outputDir = "ExtractedFiles";       // Destination folder

        ExtractEmbeddedFiles(pdfPath, outputDir);
    }
}
