using System;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "sample.pdf";

            // Verify that the PDF file exists before attempting to read its metadata.
            if (!System.IO.File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // PdfFileInfo implements IDisposable (via SaveableFacade), so we wrap it in a using block.
            using (PdfFileInfo info = new PdfFileInfo(inputPath))
            {
                // Retrieve the Author metadata; if it is not set, display a placeholder.
                string author = info.Author ?? "(no author set)";

                // Output the author information to the console.
                Console.WriteLine($"Author: {author}");
            }
        }
    }
}
