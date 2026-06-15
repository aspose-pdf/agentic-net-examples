using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Count annotations before deletion
            int beforeCount = GetTotalAnnotations(editor.Document);
            Console.WriteLine($"Annotations before deletion: {beforeCount}");

            // Delete all annotations in the document
            editor.DeleteAnnotations();

            // Count annotations after deletion
            int afterCount = GetTotalAnnotations(editor.Document);
            Console.WriteLine($"Annotations after deletion: {afterCount}");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Processed file saved as '{outputPath}'.");
    }

    // Helper method to sum annotation counts across all pages (1‑based indexing)
    static int GetTotalAnnotations(Document doc)
    {
        int total = 0;
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            total += doc.Pages[i].Annotations.Count;
        }
        return total;
    }
}