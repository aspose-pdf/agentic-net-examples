using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // 1. Add a visual heading on a target page (e.g., page 3)
            // ------------------------------------------------------------
            int targetPageNumber = 3; // 1‑based index
            if (targetPageNumber > doc.Pages.Count)
                targetPageNumber = doc.Pages.Count; // fallback to last page

            Page targetPage = doc.Pages[targetPageNumber];

            // Create a TextFragment that will appear as the heading
            TextFragment visualHeading = new TextFragment("Chapter 1: Introduction");
            visualHeading.TextState.FontSize = 20;
            visualHeading.TextState.FontStyle = FontStyles.Bold;
            visualHeading.Position = new Position(50, targetPage.PageInfo.Height - 50); // top‑left margin
            targetPage.Paragraphs.Add(visualHeading);

            // ------------------------------------------------------------
            // 2. Insert a new page at the beginning to hold the TOC
            // ------------------------------------------------------------
            doc.Pages.Insert(1);
            Page tocPage = doc.Pages[1];

            // Create TOC info (title of the TOC page)
            TocInfo tocInfo = new TocInfo();
            TextFragment tocTitle = new TextFragment("Table of Contents");
            tocTitle.TextState.FontSize = 24;
            tocTitle.TextState.FontStyle = FontStyles.Bold;
            tocInfo.Title = tocTitle;
            tocPage.TocInfo = tocInfo;

            // ------------------------------------------------------------
            // 3. Create a Heading object for the TOC entry
            // ------------------------------------------------------------
            // Level 1 heading (can be 1‑6)
            Heading tocEntry = new Heading(1);
            tocEntry.Text = "Chapter 1: Introduction";
            // Link the TOC entry to the target page
            tocEntry.DestinationPage = targetPage;
            // Associate the entry with the TOC page
            tocEntry.TocPage = tocPage;
            // Position the entry on the TOC page (simple vertical list)
            tocEntry.Top = tocPage.PageInfo.Height - 100; // adjust as needed
            // Add the heading to the TOC page
            tocPage.Paragraphs.Add(tocEntry);

            // ------------------------------------------------------------
            // 4. Save the updated PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with heading and TOC saved to '{outputPath}'.");
    }
}