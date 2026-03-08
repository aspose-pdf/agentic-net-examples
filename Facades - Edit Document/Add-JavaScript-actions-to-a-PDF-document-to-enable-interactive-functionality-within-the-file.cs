using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color
using Aspose.Pdf.Facades;          // PdfContentEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the facade that edits PDF content
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Add JavaScript actions that run on document open and close events
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen,
            "app.alert('Document opened');");
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentClose,
            "app.alert('Good-bye!');");

        // Create a clickable area on page 1 that runs JavaScript when clicked
        Rectangle linkRect = new Rectangle(100, 500, 300, 550); // left, top, width, height
        editor.CreateJavaScriptLink(
            "app.alert('Hello from link!');", // JavaScript code
            linkRect,                         // clickable rectangle
            1,                                // page number (1‑based)
            Color.Red);                       // rectangle border color

        // Save the modified PDF
        editor.Save(outputPath);

        Console.WriteLine($"JavaScript actions added and saved to '{outputPath}'.");
    }
}