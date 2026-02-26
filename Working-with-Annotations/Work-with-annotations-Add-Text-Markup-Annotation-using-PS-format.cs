using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.ps";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the specified page and rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);
            highlight.Color    = Aspose.Pdf.Color.Yellow;   // Use Aspose.Pdf.Color (cross‑platform)
            highlight.Contents = "Highlighted text";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the document as PostScript; must pass explicit SaveOptions for non‑PDF formats
            PsSaveOptions psOptions = new PsSaveOptions();
            doc.Save(outputPath, psOptions);
        }

        Console.WriteLine($"Annotated document saved as PostScript to '{outputPath}'.");
    }
}