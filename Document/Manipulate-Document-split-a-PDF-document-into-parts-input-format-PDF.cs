using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file
        const string inputPath = "input.pdf";

        // Directory where split parts will be saved
        const string outputDir = "output_parts";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the source PDF document
        Document sourceDoc = new Document(inputPath);

        // Define how many pages each split part should contain
        const int pagesPerPart = 5; // adjust as needed

        int totalPages = sourceDoc.Pages.Count;
        int partNumber = 1;

        // Iterate over the source pages in chunks
        for (int start = 1; start <= totalPages; start += pagesPerPart)
        {
            // Create a new document for the current part
            Document partDoc = new Document();

            // Determine the last page index for this part
            int end = Math.Min(start + pagesPerPart - 1, totalPages);

            // Copy the selected pages into the part document
            for (int p = start; p <= end; p++)
            {
                // Adding a page from another document clones it internally
                partDoc.Pages.Add(sourceDoc.Pages[p]);
            }

            // Build the output file name
            string partPath = Path.Combine(outputDir, $"part_{partNumber}.pdf");

            // Save the split part (uses the provided document-save rule)
            partDoc.Save(partPath);

            Console.WriteLine($"Saved part {partNumber}: {partPath}");
            partNumber++;
        }
    }
}