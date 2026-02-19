using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output folder.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: SplitPdf <input.pdf> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Validate input file.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Ensure output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Load the source PDF document.
        Document srcDoc = new Document(inputPath);

        // Iterate through all pages (Aspose.Pdf collections are 1‑based).
        for (int i = 1; i <= srcDoc.Pages.Count; i++)
        {
            // Create a new empty PDF document.
            Document singlePageDoc = new Document();

            // Remove the default empty page that the constructor adds.
            singlePageDoc.Pages.Delete(1);

            // Add the current page from the source document.
            // This copies the page into the new document.
            singlePageDoc.Pages.Add(srcDoc.Pages[i]);

            // Build the output file name for this page.
            string outPath = Path.Combine(outputFolder, $"page_{i}.pdf");

            // Save the single‑page document (uses the provided save rule).
            singlePageDoc.Save(outPath);

            Console.WriteLine($"Saved page {i} to {outPath}");
        }
    }
}