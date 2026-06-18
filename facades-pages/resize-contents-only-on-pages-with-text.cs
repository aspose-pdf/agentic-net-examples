using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF to inspect pages for text content
            List<int> pagesWithText = new List<int>();
            using (Document doc = new Document(inputPath))
            {
                // Iterate using 1‑based page indexing
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages[i].Accept(absorber);

                    // If any text is present, record the page index
                    if (!string.IsNullOrWhiteSpace(absorber.Text))
                    {
                        pagesWithText.Add(i);
                    }
                }
            }

            if (pagesWithText.Count > 0)
            {
                // Resize only the pages that contain text
                PdfFileEditor fileEditor = new PdfFileEditor();

                // Example resize parameters: new width = 200, new height = 300 (default space units)
                bool resized = fileEditor.ResizeContents(
                    inputPath,
                    outputPath,
                    pagesWithText.ToArray(),
                    200,   // new contents width
                    300);  // new contents height

                Console.WriteLine(resized
                    ? $"Resized pages {string.Join(", ", pagesWithText)} and saved to '{outputPath}'."
                    : $"Resize operation failed for '{inputPath}'.");
            }
            else
            {
                // No pages contain text – copy the original file unchanged
                File.Copy(inputPath, outputPath, true);
                Console.WriteLine($"No text found on any page. Original file copied to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}