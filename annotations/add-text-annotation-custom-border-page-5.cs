using System;
using System.IO;
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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                if (doc.Pages.Count < 5)
                {
                    Console.Error.WriteLine("Document contains fewer than 5 pages.");
                    return;
                }

                Page page = doc.Pages[5];

                // Define annotation rectangle (lower‑left x/y, upper‑right x/y)
                Rectangle rect = new Rectangle(100, 500, 300, 550);

                // Create the TextAnnotation
                TextAnnotation txtAnn = new TextAnnotation(page, rect)
                {
                    Title = "Note",
                    Contents = "Custom border annotation on page 5.",
                    Open = true,
                    Icon = TextIcon.Note
                };

                // Set border colour via the annotation's Color property (Border has no Color property)
                txtAnn.Color = Color.Red;

                // Set custom border width – Border requires the parent annotation instance
                txtAnn.Border = new Border(txtAnn) { Width = 2 };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(txtAnn);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
