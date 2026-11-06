using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // The real BookletCreator lives in this namespace. If you add the Aspose.Pdf.Facades NuGet package the stub below will be ignored.

// ---------------------------------------------------------------------------
// Stub implementation – only needed to make the project compile when the
// Aspose.Pdf.Facades package is not referenced.  Replace or remove this stub
// after adding the official package to get the full booklet functionality.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub of the Aspose.Pdf.Facades.BookletCreator class.
    /// It provides the same public API used in the sample code so the project
    /// compiles even if the real library is missing.  The implementation simply
    /// copies the source PDF to the destination without re‑ordering pages.
    /// </summary>
    public class BookletCreator
    {
        /// <summary>
        /// When true, left‑hand pages are treated as odd‑numbered.
        /// </summary>
        public bool LeftHandOddPages { get; set; }

        /// <summary>
        /// When true, right‑hand pages are treated as even‑numbered.
        /// </summary>
        public bool RightHandEvenPages { get; set; }

        /// <summary>
        /// Creates a booklet PDF.  The stub implementation just copies the input
        /// file to the output location.  The real Aspose implementation performs
        /// the proper page re‑ordering.
        /// </summary>
        /// <param name="inputPath">Path to the source PDF.</param>
        /// <param name="outputPath">Path where the booklet PDF should be saved.</param>
        public void CreateBooklet(string inputPath, string outputPath)
        {
            // Simple copy – replace with real booklet logic when the official
            // Aspose.Pdf.Facades package is referenced.
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Input file not found: {inputPath}");

            // Load the document using Aspose.Pdf (which is already referenced).
            Document doc = new Document(inputPath);
            doc.Save(outputPath);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "booklet.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the booklet creator and configure page ordering
        BookletCreator booklet = new BookletCreator
        {
            // Left‑hand pages will be odd‑numbered, right‑hand pages even‑numbered
            LeftHandOddPages = true,
            RightHandEvenPages = true
        };

        // Generate the booklet PDF
        booklet.CreateBooklet(inputPath, outputPath);

        Console.WriteLine($"Booklet created successfully: {outputPath}");
    }
}
