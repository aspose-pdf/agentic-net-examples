using System;
using System.IO;
using Aspose.Pdf;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Path to the source PDF – adjust as needed or pass via command‑line arguments.
            string inputPath = "input.pdf";
            string outputPath = "output.pdf";

            // Validate that the source file exists before attempting to load it.
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: The file '{inputPath}' was not found.");
                return;
            }

            try
            {
                using (Document document = new Document(inputPath))
                {
                    // Ensure the document has at least 12 pages.
                    if (document.Pages.Count < 12)
                    {
                        Console.WriteLine("Error: The PDF does not contain 12 pages.");
                        return;
                    }

                    // Copy the MediaBox from page 8 to page 12.
                    // Aspose.Pdf uses 1‑based indexing for pages.
                    Rectangle mediaBox = document.Pages[8].MediaBox;
                    document.Pages[12].MediaBox = mediaBox;

                    // Save the modified PDF.
                    document.Save(outputPath);
                    Console.WriteLine($"Successfully saved the updated PDF to '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
