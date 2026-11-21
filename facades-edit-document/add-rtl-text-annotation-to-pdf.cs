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

        // Define the annotation rectangle (left, bottom, right, top) in points.
        // Width = 200, Height = 100 → right = left + 200, top = bottom + 100
        var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

        // Right‑to‑left Unicode text (Arabic) to verify proper rendering.
        string rtlContent = "مرحبا بالعالم"; // "Hello World" in Arabic
        string title      = "ملاحظة";        // "Note" in Arabic

        // Load the PDF document.
        Document doc = new Document(inputPath);
        // Get the first page (1‑based index).
        Page page = doc.Pages[1];

        // Create a text annotation with RTL content.
        var textAnnotation = new TextAnnotation(page, rect)
        {
            Title    = title,
            Contents = rtlContent,
            Open     = true,               // annotation window opened by default
            Icon     = TextIcon.Note       // standard note icon
        };

        // Add the annotation to the page.
        page.Annotations.Add(textAnnotation);

        // Save the updated PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Text annotation with RTL language added to '{outputPath}'.");
    }
}
