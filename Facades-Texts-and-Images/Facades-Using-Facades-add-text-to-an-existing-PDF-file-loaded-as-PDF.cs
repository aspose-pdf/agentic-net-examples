using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using System.Drawing; // Required for Rectangle (GDI+)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor facade to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPath);

            // Define the rectangle where the text will appear (x, y, width, height)
            // Use System.Drawing.Rectangle to match the expected parameter type.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 50);

            // Add a text annotation to the first page.
            // Parameters: rectangle, text, font name, isBold, font color (as string), font size.
            editor.CreateText(rect, "Added via Facade", "Helvetica", false, "Blue", 12);

            // Save the modified PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text added successfully. Output saved to '{outputPath}'.");
    }
}