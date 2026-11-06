using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "annotated.pdf";
        const string annotationText = "Sample annotation";

        // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Perform the annotation operation without blocking the calling thread
        await AddTextAnnotationAsync(inputPdf, outputPdf, annotationText, rect);

        Console.WriteLine($"Annotation added and saved to '{outputPdf}'.");
    }

    // Wrap the annotation logic in Task.Run for asynchronous, non‑blocking execution
    static Task AddTextAnnotationAsync(string sourcePath, string destinationPath, string text, Aspose.Pdf.Rectangle rect)
    {
        return Task.Run(() =>
        {
            // Load the source PDF
            Document doc = new Document(sourcePath);

            // Get the first page (adjust if needed)
            Page page = doc.Pages[1];

            // Create a TextAnnotation bound to the page and rectangle
            TextAnnotation txtAnn = new TextAnnotation(page, rect)
            {
                // Set the annotation's displayed text
                Contents = text
                // Optional: set additional properties (color, opacity, etc.)
                // Color = Color.Yellow,
                // Opacity = 0.5,
                // Title = "Note"
            };

            // Add the annotation to the page
            page.Annotations.Add(txtAnn);

            // Save the modified PDF to the destination path
            doc.Save(destinationPath);
        });
    }
}
