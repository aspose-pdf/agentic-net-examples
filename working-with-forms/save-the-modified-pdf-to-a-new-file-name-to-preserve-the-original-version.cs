using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the original and the new PDF files
        const string inputPath  = "original.pdf";
        const string outputPath = "modified_copy.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the original PDF, modify it, and save to a new file name
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example modification: add a simple text annotation on the first page
            Page firstPage = pdfDoc.Pages[1]; // 1‑based indexing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(firstPage, rect)
            {
                Title    = "Note",
                Contents = "Modified PDF copy",
                Open     = true,
                Color    = Aspose.Pdf.Color.Yellow
            };
            firstPage.Annotations.Add(txtAnn);

            // Save the modified document to a new file, preserving the original
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}