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
        const string xfdfPath   = "annotations.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a text annotation to the first page (pages are 1‑based)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "This is a text annotation added via Aspose.Pdf.",
                Open     = true,
                Icon     = TextIcon.Note
            };
            doc.Pages[1].Annotations.Add(annotation);

            // Export all text annotations to XFDF using a FileStream
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // The core API provides ExportAnnotationsToXfdf for XFDF export.
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
        Console.WriteLine($"Annotations exported to XFDF file '{xfdfPath}'.");
    }
}
