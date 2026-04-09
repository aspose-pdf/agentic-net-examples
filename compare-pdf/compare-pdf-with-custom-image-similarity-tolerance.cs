// ==================== Program.cs ====================
using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files containing scanned images
        const string pdfPath1 = "scanned1.pdf";
        const string pdfPath2 = "scanned2.pdf";

        // Directory where comparison images will be saved
        const string outputDir = "ComparisonResults";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Ensure the source PDFs exist – create simple placeholder PDFs if they are missing
        EnsurePdfExists(pdfPath1);
        EnsurePdfExists(pdfPath2);

        // Load the two PDF documents
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create a GraphicalPdfComparer instance
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // ---------------------------------------------------------------------
            // Set a custom image‑similarity tolerance.
            // The ImageSimilarityThreshold property is available from Aspose.Pdf v23.10.
            // If you are using an older version the property will be ignored and the
            // default tolerance will be applied.
            // ---------------------------------------------------------------------
            ComparisonOptions options = new ComparisonOptions
            {
                ImageSimilarityThreshold = 0.05 // 5 % tolerance
            };
            comparer.ComparisonOptions = options;

            // Compare the documents and save the visual differences as PNG images.
            comparer.CompareDocumentsToImages(
                doc1,
                doc2,
                outputDir,
                "diff_",          // Prefix for generated image files
                ImageFormat.Png    // Output image format
            );
        }

        Console.WriteLine("Comparison completed. Results saved to: " + Path.GetFullPath(outputDir));
    }

    /// <summary>
    /// Creates a minimal PDF file with a single blank page if the specified file does not exist.
    /// This prevents a FileNotFoundException during the demo run.
    /// </summary>
    private static void EnsurePdfExists(string path)
    {
        if (!File.Exists(path))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a single empty page
                doc.Save(path);
            }
        }
    }
}

// ==================== AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig ====================
// This file is intentionally left empty. It exists solely to satisfy the project file
// reference that expects a source file named "AsposePdfApi.GeneratedMSBuildEditorConfig.editorconfig".
// No code is required here.
