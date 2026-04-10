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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the content editor and bind the PDF document
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (x, y, width, height)
        System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

        // Create a text annotation on page 1
        // Parameters: rectangle, contents, title, open flag, icon name, page number
        editor.CreateText(annotRect,
                          "This is a note with light gray background.",
                          "Note Title",
                          true,
                          "Note",
                          1);

        // Retrieve the created annotation (first annotation on page 1)
        Page page = editor.Document.Pages[1];
        if (page.Annotations.Count > 0)
        {
            // Annotation collections are 1‑based
            Annotation ann = page.Annotations[1];
            if (ann is TextAnnotation textAnn)
            {
                // Apply a light gray background color to the annotation
                textAnn.Color = Aspose.Pdf.Color.LightGray;
            }
        }

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Saved PDF with light gray text annotation to '{outputPath}'.");
    }
}