using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string searchString = "target phrase";      // string to locate
        const string outputFolder = "Occurrences";        // folder for results

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Open the source document
        using (Document srcDoc = new Document(inputPdfPath))
        {
            int occurrenceIndex = 1;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
            {
                Page srcPage = srcDoc.Pages[pageNum];

                // Search for the required phrase on the current page
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchString);
                srcPage.Accept(absorber);

                // Process each found fragment
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    // Create a new PDF that will contain only this occurrence
                    using (Document outDoc = new Document())
                    {
                        // Add a blank page with the same size as the source page
                        Page outPage = outDoc.Pages.Add();
                        outPage.PageInfo.Width = srcPage.PageInfo.Width;
                        outPage.PageInfo.Height = srcPage.PageInfo.Height;

                        // Clone the fragment (text, position, style)
                        TextFragment newFragment = new TextFragment(fragment.Text);
                        newFragment.Position = fragment.Position; // preserve location
                        newFragment.TextState.Font = fragment.TextState.Font;
                        newFragment.TextState.FontSize = fragment.TextState.FontSize;
                        newFragment.TextState.ForegroundColor = fragment.TextState.ForegroundColor;
                        newFragment.TextState.BackgroundColor = fragment.TextState.BackgroundColor;
                        newFragment.TextState.Underline = fragment.TextState.Underline;
                        // Note: TextState does not expose a Strikeout property in the current Aspose.Pdf API.
                        // If strike‑through styling is required, it must be applied via other means (e.g., using annotations).

                        // Add the fragment to the new page
                        outPage.Paragraphs.Add(newFragment);

                        // Save the occurrence as a separate PDF file
                        string outPath = Path.Combine(outputFolder, $"occurrence_{occurrenceIndex}.pdf");
                        outDoc.Save(outPath);
                        Console.WriteLine($"Saved occurrence {occurrenceIndex} to '{outPath}'");
                    }

                    occurrenceIndex++;
                }
            }

            if (occurrenceIndex == 1)
                Console.WriteLine("No occurrences found.");
        }
    }
}
