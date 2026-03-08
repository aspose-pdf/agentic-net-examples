using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document.
        Document pdfDoc = new Document(inputPath);

        // Ensure the document has at least one page (pages are 1‑based).
        if (pdfDoc.Pages.Count == 0)
        {
            Console.Error.WriteLine("The PDF contains no pages.");
            return;
        }

        // Work with the first page.
        Page page = pdfDoc.Pages[1];

        // ---------- Text (sticky‑note) annotation ----------
        // Rectangle(left, bottom, right, top) – coordinates are in points.
        Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);
        TextAnnotation textAnn = new TextAnnotation(page, textRect)
        {
            Title = "Author",
            Contents = "Sample note",
            // Optional visual tweaks
            Color = Aspose.Pdf.Color.Yellow
        };
        page.Annotations.Add(textAnn);

        // ---------- Web‑link annotation ----------
        Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
        // In recent Aspose.Pdf versions the correct action class is GoToURIAction.
        LinkAnnotation linkAnn = new LinkAnnotation(page, linkRect)
        {
            Action = new GoToURIAction("https://www.example.com")
        };
        page.Annotations.Add(linkAnn);

        // Save the modifications back into the original file.
        pdfDoc.Save(inputPath);
    }
}
