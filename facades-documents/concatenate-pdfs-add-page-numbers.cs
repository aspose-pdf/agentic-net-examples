using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] sourceFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };
        // Path for the combined PDF (will be overwritten after adding page numbers)
        const string outputPath = "combined_with_page_numbers.pdf";

        // Verify that all source files exist
        foreach (string path in sourceFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Source file not found: {path}");
                return;
            }
        }

        // ---------- Concatenate PDFs ----------
        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
        PdfFileEditor editor = new PdfFileEditor();
        // Concatenate all source PDFs into the output file
        editor.Concatenate(sourceFiles, outputPath);

        // ---------- Add page numbers ----------
        // Open the concatenated PDF inside a using block for deterministic disposal
        using (Document doc = new Document(outputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a text fragment containing the page number
                TextFragment pageNumber = new TextFragment(i.ToString());

                // Set visual appearance
                pageNumber.TextState.FontSize = 12;
                pageNumber.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                pageNumber.TextState.HorizontalAlignment = HorizontalAlignment.Center;

                // Position the fragment at the bottom center of the page
                double x = page.PageInfo.Width / 2;   // center horizontally
                double y = 20;                        // 20 points from the bottom edge
                pageNumber.Position = new Position(x, y);

                // Add the fragment to the page's paragraph collection
                page.Paragraphs.Add(pageNumber);
            }

            // Save the modified document (overwrites the same file)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDFs concatenated and page numbers added: {outputPath}");
    }
}