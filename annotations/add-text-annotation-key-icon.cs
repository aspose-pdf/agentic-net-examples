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
                // Ensure there is at least one page
                Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

                // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the TextAnnotation with required properties
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title = "Note",
                    Contents = "Sample annotation",
                    Open = false,          // annotation is closed by default
                    Icon = TextIcon.Key    // use the Key icon
                };

                // Add the annotation to the page
                page.Annotations.Add(annotation);

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