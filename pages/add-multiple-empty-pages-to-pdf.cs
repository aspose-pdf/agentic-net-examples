using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF (existing document) and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // List defining how many empty pages to add in each step
        // Example: add 2 pages, then 3 pages, then 1 page
        List<int> pagesToAdd = new List<int> { 2, 3, 1 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document (using rule: load with path)
            using (Document doc = new Document(inputPath))
            {
                // Iterate over the list and add the specified number of empty pages sequentially
                foreach (int count in pagesToAdd)
                {
                    for (int i = 0; i < count; i++)
                    {
                        // Add() creates an empty page using the most common page size in the document
                        doc.Pages.Add();
                    }
                }

                // Save the modified document (using rule: save with path)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully added pages. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}