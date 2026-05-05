using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string title = "User Comment";
        const string contents = "This is a sticky note added via Aspose.Pdf.Facades.";
        // Define the annotation rectangle (x, y, width, height)
        Rectangle annotRect = new Rectangle(100, 500, 200, 100);
        const string icon = "Comment"; // Valid icons: Comment, Key, Note, Help, NewParagraph, Paragraph, Insert
        bool open = false; // Show the note closed initially

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the content editor facade
            PdfContentEditor editor = new PdfContentEditor();
            // Bind the source PDF document
            editor.BindPdf(inputPath);
            // Create a sticky note (text annotation) on the first page
            editor.CreateText(annotRect, title, contents, open, icon, 1);
            // Save the modified PDF
            editor.Save(outputPath);
            Console.WriteLine($"Sticky note added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}