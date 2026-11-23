using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath = "annotations.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load PDF, add sticky annotation, export XFDF, and save PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("PDF contains no pages.");
                return;
            }

            // Create a sticky note (TextAnnotation) on the first page
            Rectangle rect = new Rectangle(100, 500, 200, 600);
            TextAnnotation sticky = new TextAnnotation(doc.Pages[1], rect)
            {
                Icon = TextIcon.Comment,
                Title = "Note",
                Contents = "This is a sticky annotation.",
                Color = Color.Yellow
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(sticky);

            // Export all annotations (including the sticky note) to XFDF using a FileStream
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine("Sticky annotation added, XFDF exported, and PDF saved.");
    }
}
