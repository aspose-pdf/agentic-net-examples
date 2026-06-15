using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        const string inputPdf = "input.pdf";
        const string xfdfFile = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(xfdfFile))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfFile}");
            return;
        }

        await ProcessAnnotationsAsync(inputPdf, xfdfFile, outputPdf);
        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }

    private static async Task ProcessAnnotationsAsync(string pdfPath, string xfdfPath, string outputPath)
    {
        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(pdfPath))
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF to the editor asynchronously
            await Task.Run(() => editor.BindPdf(doc));

            // Delete all existing annotations asynchronously
            await Task.Run(() => editor.DeleteAnnotations());

            // Import annotations from an XFDF file asynchronously
            await Task.Run(() => editor.ImportAnnotationsFromXfdf(xfdfPath));

            // Flatten all annotations asynchronously (optional step)
            await Task.Run(() => editor.FlatteningAnnotations());

            // Save the modified PDF asynchronously
            await Task.Run(() => editor.Save(outputPath));

            // No explicit Close needed; the using statement disposes the editor.
        }
    }
}