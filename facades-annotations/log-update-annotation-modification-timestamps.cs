using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the annotation editor and bind the PDF file
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document object
            Document doc = editor.Document;

            // Iterate over all pages and their annotations
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation ann = page.Annotations[annIdx];

                    // Log the previous modification timestamp
                    Console.WriteLine(
                        $"Page {pageNum}, Annotation {annIdx}, Type {ann.GetType().Name}, Previous Modified: {ann.Modified}");

                    // Update the Modified property to the current time
                    ann.Modified = DateTime.Now;
                }
            }

            // Save the PDF with updated annotation timestamps
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation timestamps updated and saved to '{outputPath}'.");
    }
}