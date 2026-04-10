using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userComment = "User provided comment goes here.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PDF content editor and bind the source PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (position and size) on the first page
            System.Drawing.Rectangle annotationRect = new System.Drawing.Rectangle(100, 700, 20, 20);

            // Create a sticky note (text annotation) on page 1
            // Parameters: rectangle, title, contents, open flag, icon name, page number
            editor.CreateText(annotationRect, "User", userComment, true, "Comment", 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Sticky note added and saved to '{outputPath}'.");
    }
}