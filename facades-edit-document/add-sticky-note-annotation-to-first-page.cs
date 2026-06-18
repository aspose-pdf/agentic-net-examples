using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // needed for System.Drawing.Rectangle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string comment = "User comment goes here.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor with the loaded document
            PdfContentEditor editor = new PdfContentEditor(doc);

            // Define the annotation rectangle (position and size)
            // System.Drawing.Rectangle expects (x, y, width, height)
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 700, 20, 20);

            // Create a sticky note (text annotation) on the first page
            // Title: "User", Contents: comment, Open: true, Icon: "Note"
            editor.CreateText(annotRect, "User", comment, true, "Note", 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sticky note annotation added and saved to '{outputPath}'.");
    }
}
