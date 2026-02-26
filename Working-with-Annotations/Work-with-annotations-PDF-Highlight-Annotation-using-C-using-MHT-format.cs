using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string mhtPath   = "input.mht";
        const string outputPdf = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"File not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF document
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Define the rectangle area for the highlight annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a highlight annotation on the first page
            HighlightAnnotation highlight = new HighlightAnnotation(doc.Pages[1], rect);
            highlight.Color   = Aspose.Pdf.Color.Yellow; // Set highlight color
            highlight.Opacity = 0.5;                     // Semi‑transparent
            highlight.Contents = "Highlighted text";

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(highlight);

            // Save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPdf}'.");
    }
}