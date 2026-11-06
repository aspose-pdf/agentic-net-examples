using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        int endPage = 5; // specify the last page to extract (inclusive)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF into a memory stream
        using (FileStream fileIn = File.OpenRead(inputPath))
        using (MemoryStream srcStream = new MemoryStream())
        {
            fileIn.CopyTo(srcStream);
            srcStream.Position = 0; // reset for reading

            // Prepare an output memory stream
            using (MemoryStream destStream = new MemoryStream())
            {
                // PdfFileEditor does not implement IDisposable; instantiate directly
                PdfFileEditor editor = new PdfFileEditor();

                // Extract pages 1 through endPage from srcStream into destStream
                editor.Extract(srcStream, 1, endPage, destStream);
                destStream.Position = 0; // reset for writing

                // Optionally write the result to a file
                using (FileStream fileOut = File.Create(outputPath))
                {
                    destStream.CopyTo(fileOut);
                }

                Console.WriteLine($"Pages 1-{endPage} saved to {outputPath}");
            }
        }
    }
}