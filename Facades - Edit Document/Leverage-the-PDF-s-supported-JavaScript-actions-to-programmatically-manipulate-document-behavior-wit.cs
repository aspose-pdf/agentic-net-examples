using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for Rectangle and Color used by PdfContentEditor methods

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the existing PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Add a JavaScript action that runs when the document is opened
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen,
            "app.alert('Document opened!');");

        // Add a JavaScript action that runs just before the document is saved
        editor.AddDocumentAdditionalAction(PdfContentEditor.DocumentWillSave,
            "app.alert('Document will be saved.');");

        // Create a clickable rectangle on page 1 that executes JavaScript when clicked
        // Rectangle parameters are (x, y, width, height) in points
        Rectangle linkRect = new Rectangle(100, 700, 200, 50);
        editor.CreateJavaScriptLink(
            "app.alert('Link clicked!');", // JavaScript code
            linkRect,                      // Active area
            1,                             // Page number (1‑based)
            Color.Blue);                   // Border color for the link rectangle

        // Save the modified PDF using the facade's Save method
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"JavaScript actions added and saved to '{outputPath}'.");
    }
}