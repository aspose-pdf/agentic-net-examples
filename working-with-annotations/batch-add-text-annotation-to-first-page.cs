using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAnnotatePdf
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where annotated PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Build the output file name (e.g., originalname_annotated.pdf)
            string fileName = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(outputFolder, $"{fileName}_annotated.pdf");

            // Open the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page firstPage = doc.Pages[1];

                // Define the annotation rectangle (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation and configure its properties
                TextAnnotation annotation = new TextAnnotation(firstPage, rect)
                {
                    Title    = "Standard Note",
                    Contents = "This is a standard text annotation added to the first page.",
                    Open     = true,
                    Icon     = TextIcon.Note,
                    Color    = Aspose.Pdf.Color.Yellow
                };

                // Add the annotation to the page's annotation collection
                firstPage.Annotations.Add(annotation);

                // Save the modified document to the output path
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotated PDF saved: {outputPath}");
        }
    }
}