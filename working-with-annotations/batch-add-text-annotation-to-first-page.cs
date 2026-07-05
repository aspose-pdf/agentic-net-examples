using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAddTextAnnotation
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where annotated PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files in the input folder (non‑recursive)
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build output file path (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Get the first page (Aspose.Pdf uses 1‑based indexing)
                Page firstPage = doc.Pages[1];

                // Define the rectangle where the annotation will appear
                // (llx, lly, urx, ury) – coordinates are in points
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation and configure its properties
                TextAnnotation annotation = new TextAnnotation(firstPage, rect)
                {
                    Title    = "Note",
                    Contents = "Standard annotation added by batch process.",
                    Open     = true,                     // annotation window opened by default
                    Icon     = TextIcon.Note,            // note icon
                    Color    = Aspose.Pdf.Color.Yellow   // background color of the annotation
                };

                // Add the annotation to the page's annotation collection
                firstPage.Annotations.Add(annotation);

                // Save the modified document to the output folder
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotated PDF saved: {outputPath}");
        }
    }
}