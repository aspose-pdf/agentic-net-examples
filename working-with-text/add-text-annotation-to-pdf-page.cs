using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation
            TextAnnotation txtAnnot = new TextAnnotation(page, rect)
            {
                Title    = "Note Title",
                Contents = "This is a text annotation.",
                Open     = true,
                Icon     = TextIcon.Note
            };

            // NOTE:
            // The current Aspose.Pdf version used in this project does NOT expose
            // an Action property nor a JavaScriptAction class on TextAnnotation.
            // Therefore attaching JavaScript to the annotation is not possible here.
            // If JavaScript actions are required, upgrade to a newer Aspose.Pdf version
            // that provides the appropriate API.

            // Add the annotation to the page
            page.Annotations.Add(txtAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with text annotation to '{outputPath}'.");
    }
}