using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at the very beginning (position 1, 1‑based indexing)
            Page blankPage = doc.Pages.Insert(1);

            // Define a page label: lowercase Roman numerals starting at 1 (i, ii, …)
            PageLabel frontMatterLabel = new PageLabel
            {
                NumberingStyle = NumberingStyle.NumeralsRomanLowercase,
                StartingValue = 1
                // No prefix needed
            };

            // Apply the label to the newly inserted page (zero‑based index 0)
            doc.PageLabels.UpdateLabel(0, frontMatterLabel);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom front‑matter label to '{outputPath}'.");
    }
}