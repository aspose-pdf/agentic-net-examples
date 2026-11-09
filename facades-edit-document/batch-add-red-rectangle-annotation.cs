using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "Input";
        // Folder where annotated PDFs will be saved
        const string outputFolder = "Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Define the rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y) in points
        var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
        // Red border color
        var borderColor = Color.Red;
        // Border width in points
        const int borderWidth = 2;

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName   = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(outputFolder, fileName);

            // Load the PDF document
            using (var doc = new Document(sourcePath))
            {
                // Add the rectangle annotation to the first page (pages are 1‑based)
                Page page = doc.Pages[1];
                var square = new SquareAnnotation(page, rect)
                {
                    // Border colour (Fill colour is not supported in this version, so we omit it)
                    Color = borderColor
                };
                // Border must be created with the owning annotation instance
                square.Border = new Border(square) { Width = borderWidth };
                page.Annotations.Add(square);

                // Save the annotated PDF
                doc.Save(targetPath);
            }
        }

        Console.WriteLine("Red rectangle annotations added to all PDFs in the Input folder.");
    }
}
