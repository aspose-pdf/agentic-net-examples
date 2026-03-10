using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // For AnnotationType enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the content editor and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // ------------------------------------------------------------
        // Add a Text (sticky‑note) annotation – supported PDF type
        // ------------------------------------------------------------
        // System.Drawing.Rectangle defines the location of the annotation on the page
        System.Drawing.Rectangle textRect = new System.Drawing.Rectangle(100, 600, 20, 20);
        // Parameters: rect, contents, author, open (true = displayed open), title, flags
        editor.CreateText(textRect, "This is a note added via PdfContentEditor.", "AuthorName", true, "Note Title", 0);

        // ------------------------------------------------------------
        // Add a Highlight annotation – supported PDF type
        // ------------------------------------------------------------
        // System.Drawing.Rectangle covering the text to be highlighted
        System.Drawing.Rectangle highlightRect = new System.Drawing.Rectangle(100, 500, 200, 20);
        // AnnotationType.Highlight has the numeric value 7
        editor.CreateMarkup(highlightRect, "Highlighted text", (int)AnnotationType.Highlight, 0, System.Drawing.Color.Yellow);

        // Save the modified document
        editor.Save(outputPath);
        editor.Close(); // Release resources

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}
