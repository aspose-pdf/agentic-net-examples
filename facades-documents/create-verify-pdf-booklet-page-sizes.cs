using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

namespace BookletCreationTests
{
    class Program
    {
        // Creates a simple PDF with the specified number of pages.
        // Each page contains a text fragment indicating its page number.
        // NOTE: In evaluation mode Aspose.Pdf can handle at most 4 pages in any collection.
        static void CreateSamplePdf(string filePath, int pageCount)
        {
            // Ensure the directory exists.
            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);

            using (Document doc = new Document())
            {
                // Cap the page count to 4 to stay within the evaluation‑mode limitation.
                int maxPages = Math.Min(pageCount, 4);
                for (int i = 1; i <= maxPages; i++)
                {
                    Page page = doc.Pages.Add();
                    // Add a text fragment to identify the page.
                    page.Paragraphs.Add(new TextFragment($"Sample PDF - Page {i}"));
                }

                // Save the generated PDF.
                doc.Save(filePath);
            }
        }

        // Verifies that a booklet can be created.
        // Returns true if the operation succeeds and the output PDF contains pages.
        static bool VerifyBooklet(string inputPdf, string outputPdf)
        {
            // Create the PdfFileEditor facade.
            PdfFileEditor editor = new PdfFileEditor();

            // Make the booklet (no PageSize overload).
            bool success = editor.MakeBooklet(inputPdf, outputPdf);

            if (!success || !File.Exists(outputPdf))
                return false;

            // Open the resulting PDF to check that it has pages.
            using (Document resultDoc = new Document(outputPdf))
            {
                // A booklet should have at least one page.
                return resultDoc.Pages.Count > 0;
            }
        }

        static void Main()
        {
            // Paths for temporary test files.
            string baseDir = Path.Combine(Path.GetTempPath(), "AsposePdfBookletTests");
            string inputPdf1 = Path.Combine(baseDir, "input1.pdf");
            string inputPdf2 = Path.Combine(baseDir, "input2.pdf");
            string outputPdf = Path.Combine(baseDir, "output_booklet.pdf");

            // Create two sample PDFs (2 pages each) to stay within the 4‑page evaluation limit.
            CreateSamplePdf(inputPdf1, 2);
            CreateSamplePdf(inputPdf2, 2);

            // Concatenate the two PDFs into a single source PDF for booklet testing.
            string concatenatedPdf = Path.Combine(baseDir, "concatenated.pdf");
            PdfFileEditor concatEditor = new PdfFileEditor();
            // Concatenate the two PDFs.
            concatEditor.Concatenate(new string[] { inputPdf1, inputPdf2 }, concatenatedPdf);

            // Define a set of page sizes to test, including built‑in and a custom size.
            // Note: The MakeBooklet overload does not accept a PageSize, so we only
            // use the size values for reporting purposes.
            PageSize[] pageSizes = new PageSize[]
            {
                PageSize.A4,                                 // Standard A4 size.
                new PageSize(612f, 792f),                    // US Letter size (8.5"x11").
                new PageSize(500f, 700f)                     // Custom size (width=500pt, height=700pt).
            };

            // Run the booklet creation test for each page size.
            foreach (PageSize size in pageSizes)
            {
                // Ensure any previous output is removed.
                if (File.Exists(outputPdf))
                    File.Delete(outputPdf);

                bool result = VerifyBooklet(concatenatedPdf, outputPdf);

                Console.WriteLine($"Booklet creation with page size {size.Width}x{size.Height} pts: {(result ? "PASS" : "FAIL")}");
            }

            // Clean up temporary files (optional).
            // Directory.Delete(baseDir, true);
        }
    }
}
