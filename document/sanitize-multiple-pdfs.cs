using System;
using System.IO;
using Aspose.Pdf;

class PdfSanitizer
{
    // Sanitizes a single PDF document by removing metadata, flattening forms,
    // removing PDF/A and PDF/UA compliance, and optimizing resources.
    private static void SanitizeDocument(Document doc)
    {
        // Remove all document metadata.
        doc.RemoveMetadata();

        // Flatten form fields (replace fields with their appearance).
        doc.Flatten();

        // Remove PDF/A compliance if present.
        doc.RemovePdfaCompliance();

        // Remove PDF/UA compliance if present.
        doc.RemovePdfUaCompliance();

        // Optimize resources (remove unused objects, merge duplicates).
        doc.OptimizeResources();
    }

    // Processes multiple PDF files, sanitizes each, and saves the cleaned version
    // to the specified output directory.
    public static void ProcessPdfs(string[] inputPdfPaths, string outputDirectory)
    {
        if (inputPdfPaths == null) throw new ArgumentNullException(nameof(inputPdfPaths));
        if (string.IsNullOrWhiteSpace(outputDirectory)) throw new ArgumentException("Output directory must be specified.", nameof(outputDirectory));

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in inputPdfPaths)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Use a using block to ensure the Document is disposed properly.
            using (Document doc = new Document(inputPath))
            {
                // Perform sanitization steps.
                SanitizeDocument(doc);

                // Build the output file path (same file name in the target folder).
                string outputPath = Path.Combine(outputDirectory, Path.GetFileName(inputPath));

                // Save the sanitized PDF. Save(string) writes a PDF regardless of extension.
                doc.Save(outputPath);
                Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
            }
        }
    }

    // Example entry point.
    static void Main()
    {
        // Example input PDF files.
        string[] pdfFiles = {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Target folder for cleaned PDFs.
        string cleanedFolder = "CleanedPdfs";

        ProcessPdfs(pdfFiles, cleanedFolder);
    }
}