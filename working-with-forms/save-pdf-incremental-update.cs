using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation on the first page
            Page page = doc.Pages[1];
            Rectangle rect = new Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "Incremental update example.",
                Color = Color.Yellow
            };
            page.Annotations.Add(txtAnn);

            // Save the document using the parameterless Save() method.
            // This performs an incremental update, preserving previous revisions.
            doc.Save(outputPath);

            // Optional: check whether the document now contains incremental updates
            bool hasIncremental = doc.HasIncrementalUpdate();
            Console.WriteLine($"Document has incremental updates: {hasIncremental}");
        }

        Console.WriteLine($"Incrementally saved PDF: '{outputPath}'.");
    }
}
