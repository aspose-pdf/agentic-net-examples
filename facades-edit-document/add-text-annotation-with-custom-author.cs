using System;
using System.IO;
using System.Drawing;               // needed for Rectangle
using Aspose.Pdf.Facades;          // PdfContentEditor and PdfAnnotationEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string author     = "John Doe";   // custom author metadata

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Add a text annotation (sticky note) using PdfContentEditor.
        // -----------------------------------------------------------------
        PdfContentEditor contentEditor = new PdfContentEditor();
        contentEditor.BindPdf(inputPath);

        // Define annotation rectangle (x, y, width, height) in points.
        Rectangle annotRect = new Rectangle(100, 500, 200, 100);

        // Create a text annotation with a placeholder title.
        // Parameters: rect, title, contents, open flag, icon name, page number.
        contentEditor.CreateText(annotRect, "PLACEHOLDER_AUTHOR", "Review this section.", true, "Note", 1);

        // Save the PDF with the new annotation to a temporary file.
        string tempPath = "temp_annotated.pdf";
        contentEditor.Save(tempPath);
        contentEditor.Close();

        // ---------------------------------------------------------------
        // Step 2: Change the annotation's author (Title) using PdfAnnotationEditor.
        // ---------------------------------------------------------------
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();
        annotationEditor.BindPdf(tempPath);

        // Modify the author from the placeholder to the actual author.
        // Page range is 1‑1 because we only added the annotation on page 1.
        annotationEditor.ModifyAnnotationsAuthor(1, 1, "PLACEHOLDER_AUTHOR", author);

        // Save the final PDF.
        annotationEditor.Save(outputPath);
        annotationEditor.Close();

        // Clean up the intermediate file.
        if (File.Exists(tempPath))
            File.Delete(tempPath);

        Console.WriteLine($"Annotation with author '{author}' added to '{outputPath}'.");
    }
}