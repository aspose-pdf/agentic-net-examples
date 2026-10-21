using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a sticky‑note (TextAnnotation) with user comment
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title    = "User Comment",                                   // annotation title
                Contents = "This is a user comment placed as a sticky note.", // comment text
                Open     = true,                                            // open by default
                Icon     = TextIcon.Note,                                   // sticky‑note icon
                Color    = Aspose.Pdf.Color.Yellow                         // background color
            };

            // Add the annotation to the page
            page.Annotations.Add(sticky);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sticky note added and saved to '{outputPath}'.");
    }
}