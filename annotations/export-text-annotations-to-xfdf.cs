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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a text annotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnnot = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "This is a sample annotation.",
                Open = true,
                Icon = TextIcon.Note
            };
            page.Annotations.Add(txtAnnot);

            // Export all text annotations to XFDF using a MemoryStream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // The correct API is ExportAnnotationsToXfdf; no XfdfSaveOptions class exists.
                doc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0;
                using (StreamReader reader = new StreamReader(xfdfStream))
                {
                    string xfdfContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XFDF:");
                    Console.WriteLine(xfdfContent);
                }
            }

            // Delete the previously added annotation
            // Use Delete(annotation) – Remove() does not guarantee removal.
            page.Annotations.Delete(txtAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
