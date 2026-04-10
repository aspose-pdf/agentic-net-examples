using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BookletTestSuite
{
    // Paths for temporary files
    private static readonly string TempFolder = Path.Combine(Path.GetTempPath(), "AsposePdfBookletTests");
    private static readonly List<string> InputFiles = new List<string>();
    private static readonly List<string> OutputFiles = new List<string>();

    static void Main()
    {
        // Ensure clean test directory
        if (Directory.Exists(TempFolder))
            Directory.Delete(TempFolder, true);
        Directory.CreateDirectory(TempFolder);

        try
        {
            // 1. Create sample input PDFs (each with up to 4 pages – evaluation mode limit)
            CreateSamplePdf("Sample1.pdf", 8);
            CreateSamplePdf("Sample2.pdf", 8);

            // 2. Define page sizes to test
            var pageSizes = new Dictionary<string, PageSize>
            {
                { "A4", PageSize.A4 },
                { "Letter", PageSize.PageLetter },
                { "Custom500x700", new PageSize(500, 700) }
            };

            // 3. Run booklet creation tests for each input file and each page size
            foreach (var inputPath in InputFiles)
            {
                foreach (var kvp in pageSizes)
                {
                    string sizeName = kvp.Key;
                    PageSize size = kvp.Value;

                    string outputPath = Path.Combine(TempFolder,
                        $"{Path.GetFileNameWithoutExtension(inputPath)}_Booklet_{sizeName}.pdf");
                    OutputFiles.Add(outputPath);

                    bool success = MakeBooklet(inputPath, outputPath, size);
                    Console.WriteLine($"MakeBooklet('{Path.GetFileName(inputPath)}', '{Path.GetFileName(outputPath)}', {sizeName}) => {success}");

                    // 4. Verify the output PDF exists and page count matches the input (within evaluation limits)
                    VerifyResult(inputPath, outputPath);
                }
            }

            Console.WriteLine("All booklet tests completed.");
        }
        finally
        {
            // Cleanup temporary files (optional)
            //foreach (var file in InputFiles) File.Delete(file);
            //foreach (var file in OutputFiles) File.Delete(file);
            //Directory.Delete(TempFolder, true);
        }
    }

    // Creates a simple PDF with the specified number of blank pages.
    // Evaluation mode of Aspose.PDF allows a maximum of 4 pages per document.
    // The method caps the page count at 4 to avoid IndexOutOfRangeException.
    private static void CreateSamplePdf(string fileName, int requestedPageCount)
    {
        string fullPath = Path.Combine(TempFolder, fileName);
        using (Document doc = new Document())
        {
            int pageCount = Math.Min(requestedPageCount, 4); // evaluation limit
            for (int i = 0; i < pageCount; i++)
            {
                // Add a blank page; content is not required for booklet logic
                doc.Pages.Add();
            }
            doc.Save(fullPath); // Save without explicit SaveOptions (PDF output)
        }
        InputFiles.Add(fullPath);
    }

    // Invokes PdfFileEditor.MakeBooklet with a custom page size
    private static bool MakeBooklet(string inputFile, string outputFile, PageSize pageSize)
    {
        // PdfFileEditor does not implement IDisposable; no using block needed
        PdfFileEditor editor = new PdfFileEditor();
        // Use the overload that accepts a PageSize
        return editor.MakeBooklet(inputFile, outputFile, pageSize);
    }

    // Opens the output PDF and checks that the page count matches the input PDF
    private static void VerifyResult(string inputFile, string outputFile)
    {
        if (!File.Exists(outputFile))
        {
            Console.WriteLine($"[FAIL] Output file not found: {outputFile}");
            return;
        }

        using (Document inputDoc = new Document(inputFile))
        using (Document outputDoc = new Document(outputFile))
        {
            int inputPages = inputDoc.Pages.Count;
            int outputPages = outputDoc.Pages.Count;

            if (inputPages == outputPages)
            {
                Console.WriteLine($"[PASS] Page count verified (Pages: {outputPages})");
            }
            else
            {
                Console.WriteLine($"[FAIL] Page count mismatch. Input: {inputPages}, Output: {outputPages}");
            }
        }
    }
}
