using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page so the document has at least one page
            doc.Pages.Add();

            // JavaScript that runs when the document is opened.
            // In PDF JavaScript, page numbers are zero‑based, so page 10 is index 9.
            doc.OpenAction = new JavascriptAction("this.pageNum = 9;");

            // Save the PDF. No SaveOptions needed because the output format is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript open action saved to '{outputPath}'.");
    }
}