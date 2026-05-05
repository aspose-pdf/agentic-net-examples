using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class TestBookletCreator
{
    // Paths to sample input PDFs (ensure these files exist in the execution folder)
    private static readonly string[] InputFiles = new[]
    {
        "sample1.pdf",
        "sample2.pdf",
        "sample3.pdf"
    };

    // Different page sizes to test – mix of predefined and custom sizes
    private static readonly (string Name, PageSize Size)[] PageSizes = new[]
    {
        ("A4", PageSize.A4),
        ("Letter", PageSize.PageLetter),
        ("Custom_5x7", new PageSize(504f, 360f)), // 5x7 inches (1 inch = 72 points)
        ("Custom_8x10", new PageSize(576f, 720f)) // 8x10 inches
    };

    static void Main()
    {
        // Create an output directory for the generated booklets
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BookletOutputs");
        Directory.CreateDirectory(outputDir);

        // Iterate over each input PDF and each page size
        foreach (string inputPath in InputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            foreach (var (sizeName, pageSize) in PageSizes)
            {
                string outputPath = Path.Combine(outputDir,
                    $"{Path.GetFileNameWithoutExtension(inputPath)}_Booklet_{sizeName}.pdf");

                bool success = CreateBooklet(inputPath, outputPath, pageSize);
                if (!success)
                {
                    Console.Error.WriteLine($"Failed to create booklet for '{inputPath}' with size '{sizeName}'.");
                    continue;
                }

                // Verify the output by loading it as a Document and checking page count
                try
                {
                    using (Document outDoc = new Document(outputPath))
                    {
                        Console.WriteLine($"SUCCESS: '{outputPath}' created. Pages: {outDoc.Pages.Count}");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Verification failed for '{outputPath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Test suite completed.");
    }

    // Creates a booklet using PdfFileEditor.MakeBooklet with a specific page size.
    // Returns true if the operation succeeded.
    private static bool CreateBooklet(string inputFile, string outputFile, PageSize pageSize)
    {
        try
        {
            // PdfFileEditor does NOT implement IDisposable, so no using block is required.
            PdfFileEditor editor = new PdfFileEditor();

            // MakeBooklet overload that accepts a custom PageSize.
            // Returns true on success.
            bool result = editor.MakeBooklet(inputFile, outputFile, pageSize);
            return result;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during MakeBooklet: {ex.Message}");
            return false;
        }
    }
}