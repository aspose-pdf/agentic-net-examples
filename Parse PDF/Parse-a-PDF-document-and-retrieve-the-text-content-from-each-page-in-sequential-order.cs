using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the input PDF file
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // List to hold extracted text for each page
        List<string> pageTexts = new List<string>();

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create a new TextAbsorber for the current page
                TextAbsorber absorber = new TextAbsorber();

                // Extract text from the specific page
                absorber.Visit(doc.Pages[i]);

                // Store the extracted text
                pageTexts.Add(absorber.Text);
            }
        }

        // Output the extracted text sequentially
        for (int i = 0; i < pageTexts.Count; i++)
        {
            Console.WriteLine($"--- Page {i + 1} ---");
            Console.WriteLine(pageTexts[i]);
            Console.WriteLine();
        }
    }
}