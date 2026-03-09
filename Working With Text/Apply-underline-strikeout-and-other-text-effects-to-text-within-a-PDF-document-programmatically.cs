using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "styled_output.pdf";
        const string searchText = "sample"; // text to style

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Find all occurrences of the target text (rule: use TextFragmentAbsorber)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            // Apply absorber to each page (1‑based indexing rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].Accept(absorber);
            }

            // Apply underline, strikeout and other effects to each found fragment
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                // Underline the text
                fragment.TextState.Underline = true;
                // Strike‑out the text
                fragment.TextState.StrikeOut = true;
                // Example of additional effect: make text bold
                fragment.TextState.FontStyle = FontStyles.Bold;
                // Example: change text color to red
                fragment.TextState.ForegroundColor = Color.FromRgb(1, 0, 0);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
    }
}