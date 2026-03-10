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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation on the first page
            Page page = doc.Pages[1]; // Pages are 1‑based (rule: page-indexing-one-based)

            // Fully qualified types avoid ambiguity (rule: fqtn-for-ambiguous-types)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Edited by Aspose.Pdf",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };

            page.Annotations.Add(txtAnn);

            // Persist the edited document (save rule: Document.Save(string))
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}