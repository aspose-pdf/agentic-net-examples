using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Define the annotation rectangle (left, bottom, right, top)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    // Create a text annotation for the current page
                    TextAnnotation annotation = new TextAnnotation(page, rect)
                    {
                        Title = $"Note {i}",
                        Contents = $"This is a text annotation on page {i}.",
                        Open = true,
                        Icon = TextIcon.Note,
                        Color = Aspose.Pdf.Color.Yellow
                    };

                    // Add the annotation to the page's annotation collection
                    page.Annotations.Add(annotation);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}