using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;   // for StampIcon enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // 1. Text annotation (sticky note) on the page
            // -------------------------------------------------
            var textRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            TextAnnotation textAnn = new TextAnnotation(page, textRect)
            {
                Title    = "Note Title",
                Contents = "This is a text annotation (sticky note).",
                Open     = true,                     // show the popup open by default
                Icon     = TextIcon.Note,            // note icon
                Color    = Aspose.Pdf.Color.Yellow   // background color of the annotation
            };
            page.Annotations.Add(textAnn);

            // -------------------------------------------------
            // 2. Link annotation pointing to an external URL
            // -------------------------------------------------
            var linkRect = new Aspose.Pdf.Rectangle(150, 650, 300, 670);
            LinkAnnotation linkAnn = new LinkAnnotation(page, linkRect)
            {
                Color = Aspose.Pdf.Color.Blue
            };
            // Use GoToURIAction instead of assigning a string to Hyperlink
            linkAnn.Action = new GoToURIAction("https://www.example.com");
            page.Annotations.Add(linkAnn);

            // -------------------------------------------------
            // 3. Rubber stamp annotation
            // -------------------------------------------------
            var stampRect = new Aspose.Pdf.Rectangle(200, 600, 250, 650);
            StampAnnotation stampAnn = new StampAnnotation(page, stampRect)
            {
                Icon  = StampIcon.Approved,               // built‑in stamp icon
                Color = Aspose.Pdf.Color.LightGreen
            };
            page.Annotations.Add(stampAnn);

            // -------------------------------------------------
            // 4. Page information annotation (metadata on the page)
            // -------------------------------------------------
            var infoRect = new Aspose.Pdf.Rectangle(50, 750, 200, 770);
            PageInformationAnnotation infoAnn = new PageInformationAnnotation(page, infoRect)
            {
                Contents = "Page information annotation",
                Color    = Aspose.Pdf.Color.LightGray
            };
            page.Annotations.Add(infoAnn);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with extra annotations saved to '{outputPath}'.");
    }
}