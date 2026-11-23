using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            Page pageFive = doc.Pages[5];

            // Collect titles of all existing annotations on page five
            StringBuilder titlesBuilder = new StringBuilder();
            foreach (Annotation ann in pageFive.Annotations)
            {
                // Title is defined on MarkupAnnotation (base for TextAnnotation, etc.)
                if (ann is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                {
                    if (titlesBuilder.Length > 0)
                        titlesBuilder.AppendLine();
                    titlesBuilder.Append(markup.Title);
                }
            }

            // Prepare the summary text (if no titles were found, note that)
            string summaryText = titlesBuilder.Length > 0
                ? titlesBuilder.ToString()
                : "No annotation titles found on page 5.";

            // Define a rectangle for the new TextAnnotation (positioned near the top‑left)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 800);

            // Create the summary TextAnnotation
            TextAnnotation summaryAnn = new TextAnnotation(pageFive, rect)
            {
                Title    = "Annotations Summary",
                Contents = summaryText,
                Open     = true,                     // Open the popup by default
                Icon     = TextIcon.Note               // Use the standard note icon
            };

            // Add the new annotation to the page
            pageFive.Annotations.Add(summaryAnn);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with summary annotation to '{outputPath}'.");
    }
}
