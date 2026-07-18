using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BookletTest
{
    static void Main()
    {
        // Input PDF files to test (ensure these files exist in the working directory)
        string[] inputFiles = { "sample1.pdf", "sample2.pdf" };

        // Directory where booklet outputs will be saved
        string outputDir = "BookletOutputs";
        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // Build a descriptive output file name (no custom page size needed for MakeBooklet overload)
            string outputPath = Path.Combine(
                outputDir,
                $"{Path.GetFileNameWithoutExtension(inputPath)}_booklet.pdf");

            // Create PdfFileEditor and generate the booklet using the overload that does NOT require a PageSize argument
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.MakeBooklet(inputPath, outputPath);
            if (!success)
            {
                Console.Error.WriteLine($"MakeBooklet failed for '{inputPath}'.");
                continue;
            }

            // Verify that the output file was created and contains pages
            if (!File.Exists(outputPath))
            {
                Console.Error.WriteLine($"Output file was not created: {outputPath}");
                continue;
            }

            using (Document outDoc = new Document(outputPath))
            {
                Console.WriteLine($"Booklet created: {outputPath}");
                Console.WriteLine($"  Page count: {outDoc.Pages.Count}");
            }
        }
    }
}
