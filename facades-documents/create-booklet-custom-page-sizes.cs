using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be processed (must exist in the working directory)
        string[] inputFiles = new[] { "input1.pdf", "input2.pdf", "input3.pdf" };

        // Custom page size definitions (width, height in points)
        var customPageSizes = new (string Name, double Width, double Height)[]
        {
            ("A4", 595, 842),      // A4 size in points
            ("Letter", 612, 792), // Letter size in points
            ("A5", 420, 595)      // A5 size in points
        };

        foreach (var inputFile in inputFiles)
        {
            if (!File.Exists(inputFile))
            {
                Console.Error.WriteLine($"Input file not found: {inputFile}");
                continue;
            }

            foreach (var (name, width, height) in customPageSizes)
            {
                // Create a temporary copy of the source PDF so we can resize its pages
                string tempFile = Path.GetTempFileName();
                File.Copy(inputFile, tempFile, true);

                // Resize every page to the desired custom dimensions
                using (var doc = new Document(tempFile))
                {
                    foreach (Page page in doc.Pages)
                    {
                        page.SetPageSize(width, height);
                    }
                    doc.Save(tempFile);
                }

                // Build the output filename – no directory paths
                string outputFile = $"output_{Path.GetFileNameWithoutExtension(inputFile)}_{name}.pdf";

                // PdfFileEditor does NOT implement IDisposable; use a plain instance
                var editor = new PdfFileEditor();
                bool success = editor.MakeBooklet(tempFile, outputFile); // overload without PageSize

                if (success && File.Exists(outputFile))
                {
                    Console.WriteLine($"Booklet created: {outputFile} (PageSize: {name})");
                }
                else
                {
                    Console.Error.WriteLine($"Failed to create booklet for {inputFile} with page size {name}");
                }

                // Clean up the temporary file
                try { File.Delete(tempFile); } catch { }
            }
        }
    }
}
