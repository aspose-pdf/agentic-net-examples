using System;
using System.IO;
using System.Text;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document contains fewer than 3 pages.");
                return;
            }

            // Get page three
            Page pageThree = doc.Pages[3];

            // Gather titles from all markup annotations on page three
            StringBuilder titles = new StringBuilder();
            foreach (Annotation annot in pageThree.Annotations)
            {
                if (annot is MarkupAnnotation markup && !string.IsNullOrEmpty(markup.Title))
                {
                    if (titles.Length > 0) titles.AppendLine();
                    titles.Append(markup.Title);
                }
            }

            // Create a FreeTextAnnotation summarising the collected titles
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 800);
            // DefaultAppearance expects System.Drawing.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            FreeTextAnnotation summary = new FreeTextAnnotation(pageThree, rect, appearance)
            {
                Title = "Summary of Annotation Titles",
                Contents = titles.ToString(),
                Justification = Justification.Left,
                Color = Aspose.Pdf.Color.LightGray
            };

            // Border.Width is an integer; set it after the annotation is created
            summary.Border = new Border(summary) { Width = 1 };

            // Add the summary annotation to page three
            pageThree.Annotations.Add(summary);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}