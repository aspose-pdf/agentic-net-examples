using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // for GoToURIAction and GoToAction

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string texFile   = "input.tex";
        const string pdfFile   = "output.pdf";

        if (!File.Exists(texFile))
        {
            Console.Error.WriteLine($"TeX source not found: {texFile}");
            return;
        }

        // Load TeX and convert to PDF
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document doc = new Document(texFile, texLoadOptions))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document has no pages after TeX conversion.");
                return;
            }

            // Create a link annotation on the first page
            Page page = doc.Pages[1]; // 1‑based indexing
            // Define the clickable rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Optional visual appearance
                Color = Aspose.Pdf.Color.Blue,
                // Tooltip text shown on hover
                Contents = "Visit Aspose website"
            };

            // Set the action – external URL example
            link.Action = new GoToURIAction("https://www.aspose.com");

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the resulting PDF
            doc.Save(pdfFile);
        }

        Console.WriteLine($"PDF with link annotation saved to '{pdfFile}'.");
    }
}