using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class BookletTests
{
    // Creates a single‑page PDF with a custom size and simple text.
    static void CreatePdfWithSize(string path, double width, double height)
    {
        // Ensure the directory exists.
        string? dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir))
        {
            Directory.CreateDirectory(dir);
        }

        using (Document doc = new Document())
        {
            // Define a custom page size (points).
            Page page = doc.Pages.Add();
            page.PageInfo.Width = width;
            page.PageInfo.Height = height;

            // Add a text fragment so the page is not empty.
            TextFragment tf = new TextFragment($"Page size: {width} x {height}");
            tf.TextState.FontSize = 12;
            tf.Position = new Position(50, height - 50);
            page.Paragraphs.Add(tf);

            doc.Save(path);
        }
    }

    // Creates a booklet from the given source PDFs and verifies the result.
    static void TestBooklet(string[] sourcePaths, string outputPath)
    {
        // Combine source PDFs into a temporary merged PDF.
        string mergedPath = Path.Combine(Path.GetTempPath(), $"merged_{Guid.NewGuid()}.pdf");
        using (Document merged = new Document())
        {
            foreach (string src in sourcePaths)
            {
                using (Document srcDoc = new Document(src))
                {
                    // Append all pages from the source document.
                    merged.Pages.Add(srcDoc.Pages);
                }
            }
            merged.Save(mergedPath);
        }

        // Create a booklet from the merged PDF.
        // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using.
        PdfFileEditor editor = new PdfFileEditor();

        // Use the overload that does not require explicit BookletSettings.
        // The correct method name is MakeBooklet.
        editor.MakeBooklet(mergedPath, outputPath);

        // Verify that the output PDF exists and its page count is a multiple of 4.
        using (Document result = new Document(outputPath))
        {
            int pageCount = result.Pages.Count;
            if (pageCount % 4 != 0)
            {
                Console.Error.WriteLine($"[FAIL] Booklet page count ({pageCount}) is not a multiple of 4.");
            }
            else
            {
                Console.WriteLine($"[PASS] Booklet created successfully with {pageCount} pages.");
            }
        }

        // Clean up temporary merged file.
        try { File.Delete(mergedPath); } catch { /* ignore */ }
    }

    static void Main()
    {
        // Define custom page sizes (width x height in points).
        var pageSizes = new List<(double width, double height)>
        {
            (595, 842),   // A4
            (612, 792),   // Letter
            (400, 600)    // Custom small size
        };

        // Create temporary PDFs with the defined sizes.
        var sourceFiles = new List<string>();
        int index = 1;
        foreach (var (w, h) in pageSizes)
        {
            string path = Path.Combine(Path.GetTempPath(), $"sample_{index}.pdf");
            CreatePdfWithSize(path, w, h);
            sourceFiles.Add(path);
            index++;
        }

        // Output booklet path.
        string bookletPath = Path.Combine(Path.GetTempPath(), $"booklet_{Guid.NewGuid()}.pdf");

        // Run the booklet test.
        TestBooklet(sourceFiles.ToArray(), bookletPath);

        // Clean up source PDFs.
        foreach (var file in sourceFiles)
        {
            try { File.Delete(file); } catch { /* ignore */ }
        }

        // Optionally delete the booklet after verification.
        // try { File.Delete(bookletPath); } catch { /* ignore */ }
    }
}
