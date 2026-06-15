using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "SplitPages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF
            using (Document srcDoc = new Document(inputPath))
            {
                int pageCount = srcDoc.Pages.Count;

                // Iterate over each page, copy it into a new document and save
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty document
                    Document pageDoc = new Document();

                    // Add the current page from the source document to the new document
                    // The Add method clones the page, so resources are copied correctly
                    pageDoc.Pages.Add(srcDoc.Pages[i]);

                    string outPath = Path.Combine(outputDir, $"Page_{i}.pdf");
                    pageDoc.Save(outPath);
                    pageDoc.Dispose();
                }
            }

            Console.WriteLine("PDF split into individual pages successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
