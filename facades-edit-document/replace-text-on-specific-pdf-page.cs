using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldText = "OldText";
        const string newText = "NewText";
        const int targetPage = 3; // 1‑based page number

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document using the high‑level API (Document)
            Document pdf = new Document(inputPath);

            // Ensure the requested page exists
            if (targetPage < 1 || targetPage > pdf.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPage} is out of range. Document has {pdf.Pages.Count} pages.");
                return;
            }

            // Create a TextFragmentAbsorber that will find the text to replace
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(oldText);
            // (Optional) make the search case‑sensitive or enable other options here
            // absorber.TextSearchOptions = new TextSearchOptions(true);

            // Search only on the specified page
            pdf.Pages[targetPage].Accept(absorber);

            // Replace each found fragment with the new text
            int replaceCount = 0;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                fragment.Text = newText;
                replaceCount++;
            }

            // Save the modified document
            pdf.Save(outputPath);

            Console.WriteLine($"Replaced '{oldText}' with '{newText}' on page {targetPage} ({replaceCount} occurrence(s)). Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
