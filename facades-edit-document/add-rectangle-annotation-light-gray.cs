using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF using the Facades API
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document object
        Document doc = editor.Document;
        Page page = doc.Pages[pageNumber];

        // Define the rectangle area for the annotation (left, bottom, right, top)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

        // Create a square (rectangle) annotation
        SquareAnnotation square = new SquareAnnotation(page, rect)
        {
            // Fill color (light gray)
            InteriorColor = Aspose.Pdf.Color.LightGray
        };

        // Set border width to 1 pt
        square.Border = new Border(square) { Width = 1 };

        // Add the annotation to the page
        page.Annotations.Add(square);

        // Save the modified PDF via the Facades editor
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}