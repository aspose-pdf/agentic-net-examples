using System;
using System.IO;
using Aspose.Pdf;

class SplitPdfProgram
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output folder (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: SplitPdfProgram <input-pdf> <output-folder>");
            return;
        }

        string inputPdfPath = args[0];
        string outputFolder = args[1];

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the source PDF document
            Document sourceDoc = new Document(inputPdfPath);

            // Iterate over each page (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= sourceDoc.Pages.Count; i++)
            {
                // Create a new empty PDF document
                Document singlePageDoc = new Document();

                // Add the current page from the source document
                singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

                // Build output file name: original name + page number
                string inputFileName = Path.GetFileNameWithoutExtension(inputPdfPath);
                string outputFilePath = Path.Combine(outputFolder, $"{inputFileName}_page_{i}.pdf");

                // Save the single‑page document using the standard save rule
                singlePageDoc.Save(outputFilePath);

                Console.WriteLine($"Saved page {i} to '{outputFilePath}'.");
            }

            Console.WriteLine("PDF splitting completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}