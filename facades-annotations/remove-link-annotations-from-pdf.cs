using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_links.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (facade) to work with annotations.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Delete all link annotations in the document.
            // The DeleteAnnotations(string) overload removes annotations of the specified type.
            // According to the task, we target "Link" annotations.
            editor.DeleteAnnotations("Link");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Link annotations removed. Output saved to '{outputPath}'.");
    }
}