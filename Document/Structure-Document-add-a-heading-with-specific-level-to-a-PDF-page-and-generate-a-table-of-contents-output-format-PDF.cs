using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Collection to store TOC entries (heading text and page number)
        List<(string Text, int PageNumber)> tocEntries = new List<(string, int)>();

        // -----------------------------------------------------------------
        // 1. Add a heading to a specific page (example uses page 2)
        // -----------------------------------------------------------------
        int targetPageNumber = 2; // change as needed

        if (targetPageNumber < 1 || targetPageNumber > pdfDocument.Pages.Count)
        {
            Console.Error.WriteLine($"Target page number {targetPageNumber} is out of range.");
            return;
        }

        Page targetPage = pdfDocument.Pages[targetPageNumber];

        // Create a heading. The constructor argument is not used for layout,
        // the actual level is set via the Level property.
        Heading heading = new Heading(0);
        heading.Text = "Chapter 1: Introduction";
        heading.Level = 1;               // Desired heading level
        heading.IsInList = true;         // Include in TOC generation (if supported)

        // Add the heading to the chosen page
        targetPage.Paragraphs.Add(heading);

        // Record the heading for the TOC
        tocEntries.Add((heading.Text, targetPageNumber));

        // -----------------------------------------------------------------
        // 2. Create a Table of Contents page at the beginning of the document
        // -----------------------------------------------------------------
        Page tocPage = pdfDocument.Pages.Insert(1); // Inserts a new blank page at position 1

        // Title for the TOC
        TextFragment tocTitle = new TextFragment("Table of Contents");
        tocTitle.TextState.FontSize = 20;
        tocTitle.TextState.FontStyle = FontStyles.Bold;
        tocTitle.Position = new Position(0, 800); // Position near the top of the page
        tocPage.Paragraphs.Add(tocTitle);

        // Add each heading as a TOC entry
        float currentY = 760; // Starting Y coordinate for the first entry
        foreach (var entry in tocEntries)
        {
            // Simple layout: "Heading text ........ pageNumber"
            TextFragment entryFragment = new TextFragment($"{entry.Text} ........ {entry.PageNumber}");
            entryFragment.TextState.FontSize = 12;
            entryFragment.Position = new Position(0, currentY);
            tocPage.Paragraphs.Add(entryFragment);
            currentY -= 20; // Move down for the next entry
        }

        // -----------------------------------------------------------------
        // 3. Save the modified PDF (output format: PDF)
        // -----------------------------------------------------------------
        pdfDocument.Save(outputPath);
    }
}