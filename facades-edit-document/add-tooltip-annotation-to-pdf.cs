using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tooltip_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Define the annotation rectangle (System.Drawing.Rectangle is required by PdfContentEditor)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 20, 20); // x, y, width, height

        // Title appears in the annotation window; Contents is shown as a tooltip on hover
        string title = "Info";
        string tooltip = "Additional information displayed when the mouse hovers over this area.";
        bool openInitially = false; // annotation window closed by default
        string icon = "Note"; // supported icons: Comment, Key, Note, Help, NewParagraph, Paragraph, Insert

        // Create a text (sticky‑note) annotation on page 1
        editor.CreateText(rect, title, tooltip, openInitially, icon, 1);

        // Save the modified document
        editor.Save(outputPath);
        editor.Close(); // release resources held by the facade

        Console.WriteLine($"Tooltip annotation added. Saved to '{outputPath}'.");
    }
}