using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string customSizePath = "custom_size.pdf";
        const string revertedPath = "reverted_size.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                int pageNumber = 1; // 1‑based page index

                // Retrieve the original size of the page
                PageSize originalSize = editor.GetPageSize(pageNumber);
                double originalWidth = originalSize.Width;
                double originalHeight = originalSize.Height;

                // Define custom dimensions (example: 500 x 700 points)
                double customWidth = 500;
                double customHeight = 700;

                // Apply custom page size to the selected page
                editor.PageSize = new PageSize((float)customWidth, (float)customHeight);
                editor.ProcessPages = new int[] { pageNumber };
                editor.ApplyChanges();

                // Save the PDF with the custom page size
                doc.Save(customSizePath);
                Console.WriteLine($"Custom sized PDF saved to '{customSizePath}'");

                // Revert to the original page size
                editor.PageSize = new PageSize((float)originalWidth, (float)originalHeight);
                editor.ProcessPages = new int[] { pageNumber };
                editor.ApplyChanges();

                // Save the PDF after reverting the size
                doc.Save(revertedPath);
                Console.WriteLine($"Reverted PDF saved to '{revertedPath}'");
            }
        }
    }
}