using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Load the PDF using PdfContentEditor (does not implement IDisposable)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Iterate through all pages and their annotations
        for (int i = 1; i <= editor.Document.Pages.Count; i++)
        {
            Page page = editor.Document.Pages[i];
            foreach (Annotation annot in page.Annotations)
            {
                // Process only Line annotations
                if (annot is LineAnnotation lineAnnot)
                {
                    // Create a new Border for the annotation (requires the parent annotation)
                    Border border = new Border(lineAnnot);

                    // Set a dashed pattern: 3 units on, 3 units off
                    border.Dash = new Dash(new int[] { 3, 3 });

                    // Apply the new border to the line annotation
                    lineAnnot.Border = border;
                }
            }
        }

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}