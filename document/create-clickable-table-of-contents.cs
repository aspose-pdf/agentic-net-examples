using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "toc.pdf";

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // Page 1 – Table of Contents
            // -------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // TOC title
            TextFragment tocTitle = new TextFragment("Table of Contents");
            tocTitle.TextState.FontSize = 20;
            tocTitle.TextState.Font = FontRepository.FindFont("Helvetica");
            tocTitle.Position = new Position(50, 800);
            tocPage.Paragraphs.Add(tocTitle);

            // -------------------------------------------------
            // Page 2 – First Section Heading
            // -------------------------------------------------
            Page headingPage1 = doc.Pages.Add();

            TextFragment heading1 = new TextFragment("Section 1: Introduction");
            heading1.TextState.FontSize = 16;
            heading1.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            heading1.Position = new Position(50, 800);
            headingPage1.Paragraphs.Add(heading1);

            // -------------------------------------------------
            // Page 3 – Second Section Heading
            // -------------------------------------------------
            Page headingPage2 = doc.Pages.Add();

            TextFragment heading2 = new TextFragment("Section 2: Details");
            heading2.TextState.FontSize = 16;
            heading2.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            heading2.Position = new Position(50, 800);
            headingPage2.Paragraphs.Add(heading2);

            // -------------------------------------------------
            // Add clickable TOC entries (LinkAnnotations)
            // -------------------------------------------------
            // Entry 1 – link to Section 1
            TextFragment entry1 = new TextFragment("1. Introduction");
            entry1.TextState.FontSize = 12;
            entry1.Position = new Position(70, 750);
            tocPage.Paragraphs.Add(entry1);

            // Rectangle covering the entry text (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(70, 730, 300, 750);
            LinkAnnotation link1 = new LinkAnnotation(tocPage, rect1);
            // Use explicit destination via GoToAction (rule: no Destination class)
            link1.Action = new GoToAction(headingPage1);
            tocPage.Annotations.Add(link1);

            // Entry 2 – link to Section 2
            TextFragment entry2 = new TextFragment("2. Details");
            entry2.TextState.FontSize = 12;
            entry2.Position = new Position(70, 720);
            tocPage.Paragraphs.Add(entry2);

            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(70, 700, 300, 720);
            LinkAnnotation link2 = new LinkAnnotation(tocPage, rect2);
            link2.Action = new GoToAction(headingPage2);
            tocPage.Annotations.Add(link2);

            // -------------------------------------------------
            // Save the PDF document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clickable Table of Contents saved to '{outputPath}'.");
    }
}