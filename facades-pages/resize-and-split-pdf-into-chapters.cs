using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "book.pdf";          // source PDF
        const string resizedPath = "book_resized.pdf";  // intermediate uniform PDF
        const string outputDir   = "Chapters";          // folder for chapter files

        // Verify input exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Step 1 – Resize every page to a uniform size (A4).
        // ------------------------------------------------------------
        const double a4Width  = 595; // points (A4 width)
        const double a4Height = 842; // points (A4 height)

        // Load the source document, adjust each page size, and save as a new file.
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width  = a4Width;
                page.PageInfo.Height = a4Height;
            }
            doc.Save(resizedPath);
        }

        // ------------------------------------------------------------
        // Step 2 – Split the resized PDF into chapter files.
        // ------------------------------------------------------------
        var chapters = new (int start, int end, string fileName)[]
        {
            (1, 10, "Chapter1.pdf"),
            (11, 20, "Chapter2.pdf"),
            (21, 30, "Chapter3.pdf")
            // Add more ranges as needed
        };

        PdfFileEditor fileEditor = new PdfFileEditor(); // does NOT implement IDisposable

        foreach (var (start, end, fileName) in chapters)
        {
            string outPath = Path.Combine(outputDir, fileName);
            // Extract the specified page range into a new PDF file.
            // Note: the correct overload order is (source, startPage, endPage, output).
            fileEditor.Extract(resizedPath, start, end, outPath);
        }

        Console.WriteLine("PDF has been resized and split into chapters successfully.");
    }
}
