using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BookletTestSuite
{
    static void Main()
    {
        // Define test inputs – ensure these files exist in the working directory
        string[] inputFiles = { "input1.pdf", "input2.pdf" };
        // Define output directory for generated booklets
        string outputDir = "BookletOutputs";
        Directory.CreateDirectory(outputDir);

        // Test with predefined page sizes
        RunBookletTest(inputFiles[0], Path.Combine(outputDir, "booklet_A4.pdf"), PageSize.A4);
        RunBookletTest(inputFiles[0], Path.Combine(outputDir, "booklet_Letter.pdf"), PageSize.PageLetter);

        // Test with a custom page size (width: 500 points, height: 700 points)
        Aspose.Pdf.PageSize customSize = new Aspose.Pdf.PageSize(500, 700);
        RunBookletTest(inputFiles[1], Path.Combine(outputDir, "booklet_Custom.pdf"), customSize);

        // Test with multiple inputs – create a booklet for each file using the same custom size
        foreach (string input in inputFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(input);
            string outPath = Path.Combine(outputDir, $"booklet_{fileName}_Custom.pdf");
            RunBookletTest(input, outPath, customSize);
        }

        Console.WriteLine("All booklet tests completed.");
    }

    static void RunBookletTest(string inputPath, string outputPath, Aspose.Pdf.PageSize pageSize)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfFileEditor does NOT implement IDisposable – instantiate directly
        PdfFileEditor editor = new PdfFileEditor();

        // Create booklet with the specified page size
        bool success = editor.MakeBooklet(inputPath, outputPath, pageSize);
        if (!success)
        {
            Console.Error.WriteLine($"MakeBooklet failed for {inputPath} -> {outputPath}");
            return;
        }

        // Verify the output file exists and inspect page count
        if (!File.Exists(outputPath))
        {
            Console.Error.WriteLine($"Output file was not created: {outputPath}");
            return;
        }

        // Open the resulting PDF to read its properties
        using (Document resultDoc = new Document(outputPath))
        {
            Console.WriteLine($"Booklet created: {outputPath}");
            Console.WriteLine($"  Page size: Width={pageSize.Width}, Height={pageSize.Height}");
            Console.WriteLine($"  Total pages: {resultDoc.Pages.Count}");
        }
    }
}