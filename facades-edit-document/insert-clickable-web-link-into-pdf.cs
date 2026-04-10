using System;
using System.IO;
using System.Drawing;               // Required for Rectangle and Color used by PdfContentEditor
using Aspose.Pdf.Facades;          // Facade API for editing PDF content

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Source PDF
        const string outputPath = "output.pdf";         // Destination PDF
        const string url        = "https://www.example.com"; // External website

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the editor (lifecycle: create)
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF into the editor (lifecycle: load)
        editor.BindPdf(inputPath);

        // Define the clickable rectangle (x, y, width, height)
        Rectangle linkRect = new Rectangle(100, 500, 200, 50);

        // Page number where the link will be placed (1‑based indexing)
        int pageNumber = 1;

        // Optional rectangle colour (System.Drawing.Color is required by the API)
        Color rectColor = Color.Red;

        // Insert the web link (feature: CreateWebLink)
        editor.CreateWebLink(linkRect, url, pageNumber, rectColor);

        // Save the modified PDF (lifecycle: save)
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Web link added and saved to '{outputPath}'.");
    }
}