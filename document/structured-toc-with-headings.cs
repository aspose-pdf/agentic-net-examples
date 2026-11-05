using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Insert a new page at the beginning to hold the Table of Contents
            Page tocPage = doc.Pages.Insert(1);

            // Common text style for headings
            TextState headingState = new TextState
            {
                FontSize = 14,
                FontStyle = FontStyles.Bold,
                Font = FontRepository.FindFont("Arial")
            };

            // Helper to create a heading and add it to the TOC page
            void AddHeading(int level, NumberingStyle numberingStyle, string title, Page destinationPage, double topPosition)
            {
                Heading heading = new Heading(level)
                {
                    IsAutoSequence = true,          // automatically generate page number reference
                    TocPage = tocPage,               // page where the TOC entry will be placed
                    DestinationPage = destinationPage,
                    Top = topPosition,
                    Style = numberingStyle          // set numbering style
                };

                // Create the visible text segment
                TextSegment segment = new TextSegment(title);
                // Apply the common style (modify the existing TextState, do NOT replace it)
                segment.TextState.FontSize = headingState.FontSize;
                segment.TextState.FontStyle = headingState.FontStyle;
                segment.TextState.Font = headingState.Font;

                heading.Segments.Add(segment);
                tocPage.Paragraphs.Add(heading);
            }

            // Determine a destination page for the example (the first page after the TOC)
            Page firstContentPage = doc.Pages.Count > 1 ? doc.Pages[2] : doc.Pages[1];

            // 1. Decimal numbering (1., 2., …)
            AddHeading(
                level: 1,
                numberingStyle: NumberingStyle.NumeralsArabic,
                title: "Chapter 1 – Introduction",
                destinationPage: firstContentPage,
                topPosition: 20);

            // 2. Upper‑roman numbering (I., II., …)
            AddHeading(
                level: 1,
                numberingStyle: NumberingStyle.NumeralsRomanUppercase,
                title: "Chapter 2 – Background",
                destinationPage: firstContentPage,
                topPosition: 50);

            // 3. Lower‑letter numbering for a sub‑heading (a., b., …)
            AddHeading(
                level: 2,
                numberingStyle: NumberingStyle.LettersLowercase,
                title: "2.1 Details",
                destinationPage: firstContentPage,
                topPosition: 80);

            // 4. No numbering (plain text)
            AddHeading(
                level: 1,
                numberingStyle: NumberingStyle.None,
                title: "Appendix A – References",
                destinationPage: firstContentPage,
                topPosition: 110);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPath}'.");
    }
}
