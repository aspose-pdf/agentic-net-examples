using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "input_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation on the first page
            Page page = doc.Pages[1]; // 1‑based indexing

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Modified PDF – original preserved.",
                Color = Aspose.Pdf.Color.Yellow,
                Open = true,
                Icon = TextIcon.Note
            };

            page.Annotations.Add(txtAnn);

            // Save to a new file name – original remains unchanged
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}