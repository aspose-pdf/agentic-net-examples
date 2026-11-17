using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction lives here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_link.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the clickable area for the link annotation
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

            // Create a LinkAnnotation and assign a JavaScript action
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // JavaScript that shows a modal alert dialog
                Action = new JavascriptAction("app.alert('This is a custom modal dialog box.');")
            };

            // Use Contents for the tooltip (LinkAnnotation has no Title property)
            link.Contents = "Show Modal Dialog";

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link annotation to '{outputPath}'.");
    }
}
