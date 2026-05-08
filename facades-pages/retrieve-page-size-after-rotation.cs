using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "sample.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Rotate the first page by 90 degrees
            editor.PageRotations[1] = 90;   // page numbers are 1‑based
            editor.ApplyChanges();          // apply the rotation to the document

            // Retrieve the rotation of the first page to verify the change
            int rotation = editor.GetPageRotation(1);
            Console.WriteLine($"Rotation of page 1 after change: {rotation} degrees");

            // Retrieve the page size after rotation
            PageSize size = editor.GetPageSize(1);
            Console.WriteLine($"Page 1 size after rotation: Width = {size.Width}, Height = {size.Height}");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}