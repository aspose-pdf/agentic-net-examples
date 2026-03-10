using System;
using System.IO;
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

        // Load the PDF – wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a sticky‑note (TextAnnotation) on the selected page
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title    = "Reviewer", // shown in the annotation title bar
                // Markdown text can be stored in Contents; PDF viewers render it as plain text
                Contents = "# Review Comments\n- Check spelling\n- Update figure 2\n\n_Approved_",
                Open     = true,               // show the note when the PDF opens
                Icon     = TextIcon.Note,       // standard sticky‑note icon
                Color    = Aspose.Pdf.Color.Yellow // optional border color
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(sticky);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sticky annotation added and saved to '{outputPath}'.");
    }
}