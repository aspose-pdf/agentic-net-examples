using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class BookletTestSuite
{
    static void Main()
    {
        // Temporary folder for test files
        string tempDir = Path.Combine(Path.GetTempPath(), "BookletTest");
        Directory.CreateDirectory(tempDir);

        // Create a set of sample PDFs
        string[] inputFiles = new string[3];
        for (int i = 0; i < inputFiles.Length; i++)
        {
            inputFiles[i] = Path.Combine(tempDir, $"input{i + 1}.pdf");
            // Use different page sizes for each sample PDF
            PageSize size = GetTestPageSize(i);
            CreateSamplePdf(inputFiles[i], $"Sample PDF {i + 1}", i + 1, size);
        }

        // Page sizes to test (used only for naming the output files)
        PageSize[] pageSizes = new PageSize[]
        {
            PageSize.A4,
            new PageSize(612, 792), // Letter size in points (8.5" x 11")
            new PageSize(500, 700)   // Custom size
        };

        // Execute booklet creation for each input PDF and each page size name
        foreach (string inputFile in inputFiles)
        {
            foreach (PageSize size in pageSizes)
            {
                string sizeName = GetPageSizeName(size);
                string outputFile = Path.Combine(tempDir, $"booklet_{Path.GetFileNameWithoutExtension(inputFile)}_{sizeName}.pdf");

                // MakeBooklet using PdfFileEditor (no PageSize overload, no using statement)
                bool success = false;
                try
                {
                    PdfFileEditor editor = new PdfFileEditor();
                    // The overload without PageSize is the only one available in current Aspose.PDF versions
                    success = editor.MakeBooklet(inputFile, outputFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"MakeBooklet exception: {ex.Message}");
                    success = false;
                }

                Console.WriteLine($"MakeBooklet('{inputFile}', '{outputFile}', {sizeName}) => {success}");

                // Verify that the output file exists and page count matches the input (or is a multiple of 4 for booklet layout)
                if (success && File.Exists(outputFile))
                {
                    using (Document outDoc = new Document(outputFile))
                    using (Document inDoc = new Document(inputFile))
                    {
                        int inPages = inDoc.Pages.Count;   // 1‑based indexing
                        int outPages = outDoc.Pages.Count;
                        Console.WriteLine($"Input pages: {inPages}, Output pages: {outPages}");
                        // Booklet conversion may add blank pages to make the total a multiple of 4
                        if (outPages % 4 != 0)
                        {
                            Console.WriteLine("Warning: output page count is not a multiple of 4 – booklet layout may be incorrect.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: booklet creation failed or output file missing.");
                }
            }
        }

        // Optional cleanup
        // Directory.Delete(tempDir, true);
    }

    // Returns a PageSize for the sample PDFs (different size per index just to vary the source PDFs)
    static PageSize GetTestPageSize(int index)
    {
        switch (index)
        {
            case 0: return PageSize.A4;
            case 1: return new PageSize(612, 792); // Letter
            case 2: return new PageSize(500, 700); // Custom
            default: return PageSize.A4;
        }
    }

    // Creates a simple PDF with the specified number of pages, a text fragment on each page, and the given page size
    static void CreateSamplePdf(string path, string title, int pageCount, PageSize pageSize)
    {
        using (Document doc = new Document())
        {
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages.Add();
                page.PageInfo.Width = pageSize.Width;
                page.PageInfo.Height = pageSize.Height;

                TextFragment tf = new TextFragment($"{title} - Page {i}");
                tf.Position = new Position(100, pageSize.Height - 100);
                page.Paragraphs.Add(tf);
            }
            doc.Save(path);
        }
    }

    // Returns a readable name for a PageSize (standard names or custom dimensions)
    static string GetPageSizeName(PageSize size)
    {
        if (size == PageSize.A4) return "A4";
        // Letter size is represented by its dimensions (612 x 792 points)
        if (size.Width == 612 && size.Height == 792) return "Letter";
        // Custom size – include width and height in the name
        return $"Custom_{size.Width}_{size.Height}";
    }
}
