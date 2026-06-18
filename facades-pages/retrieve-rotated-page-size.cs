using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Rotate the first page by 90 degrees (valid values: 0, 90, 180, 270)
                editor.Rotation = 90;
                editor.ApplyChanges();

                // Retrieve the page size after rotation
                PageSize size = editor.GetPageSize(1);
                int rotation = editor.GetPageRotation(1);

                // Adjust dimensions based on rotation (swap width/height for 90° or 270°)
                double width = size.Width;
                double height = size.Height;
                if (rotation == 90 || rotation == 270)
                {
                    double temp = width;
                    width = height;
                    height = temp;
                }

                Console.WriteLine($"Page 1 rotation: {rotation} degrees");
                Console.WriteLine($"Effective page size: {width} x {height}");

                // Save the rotated PDF (optional)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}