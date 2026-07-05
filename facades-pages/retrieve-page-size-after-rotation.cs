using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const int pageNumber = 1; // 1‑based index

        // ------------------------------------------------------------
        // Ensure a source PDF exists – create a simple one if it does not.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                // Add a single page with default size (A4 – 595 x 842 points).
                Page page = doc.Pages.Add();
                // Optional: add some visible content so the page is not empty.
                page.Paragraphs.Add(new TextFragment("Sample page for rotation test"));
                doc.Save(inputPath);
                Console.WriteLine($"Created placeholder PDF '{inputPath}'.");
            }
        }

        // ------------------------------------------------------------
        // Rotate the page and retrieve its size after rotation.
        // ------------------------------------------------------------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Show the current rotation of the target page.
            int originalRotation = editor.GetPageRotation(pageNumber);
            Console.WriteLine($"Original rotation of page {pageNumber}: {originalRotation}°");

            // Restrict the operation to the desired page (good practice).
            editor.ProcessPages = new int[] { pageNumber };

            // Apply a 90° clockwise rotation.
            editor.Rotation = 90; // valid values: 0, 90, 180, 270
            editor.ApplyChanges();

            // Retrieve the logical page size after rotation.
            PageSize size = editor.GetPageSize(pageNumber);
            // The GetPageSize method returns the media box dimensions *before* rotation.
            // When the page is rotated by 90° or 270°, width/height are swapped.
            double effectiveWidth = size.Width;
            double effectiveHeight = size.Height;
            int finalRotation = (originalRotation + editor.Rotation) % 360;
            if (finalRotation == 90 || finalRotation == 270)
            {
                // Swap dimensions to reflect the visual orientation.
                (effectiveWidth, effectiveHeight) = (effectiveHeight, effectiveWidth);
            }
            Console.WriteLine($"Size of page {pageNumber} after rotation: {effectiveWidth} x {effectiveHeight}");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine("Operation completed.");
    }
}
