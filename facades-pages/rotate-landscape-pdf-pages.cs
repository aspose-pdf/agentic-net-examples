using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine which pages are in landscape orientation.
            // Landscape is inferred when page width > page height.
            var pageRotations = new Dictionary<int, int>();

            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                double width  = page.Rect.URX - page.Rect.LLX;
                double height = page.Rect.URY - page.Rect.LLY;

                if (width > height)               // Landscape page detected
                    pageRotations[i] = 90;        // Rotate 90 degrees clockwise
            }

            // If no pages need rotation, simply save the original document
            if (pageRotations.Count == 0)
            {
                doc.Save(outputPath);
                Console.WriteLine("No landscape pages found. Document saved unchanged.");
                return;
            }

            // Use PdfPageEditor (Facade) to apply the rotations
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);                 // Bind the loaded document
            editor.PageRotations = pageRotations; // Assign the rotation map (Dictionary<int,int>)
            editor.ApplyChanges();               // Commit changes to the document

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
