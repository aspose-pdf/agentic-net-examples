using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAnnotate
{
    static void Main()
    {
        // Folder containing PDFs to process
        const string inputFolder = @"C:\PdfFolder";
        // Folder where annotated PDFs will be saved
        const string outputFolder = @"C:\PdfFolder\Annotated";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files in the input folder
        foreach (string pdfPath in Directory.EnumerateFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count < 1)
                    {
                        Console.WriteLine($"Skipping empty document: {Path.GetFileName(pdfPath)}");
                        continue;
                    }

                    // Get the first page (1‑based indexing)
                    Page firstPage = doc.Pages[1];

                    // Define the annotation rectangle (llx, lly, urx, ury)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    // Create a text annotation using the rectangle
                    TextAnnotation annotation = new TextAnnotation(firstPage, rect)
                    {
                        Title    = "Note",
                        Contents = "Standard annotation added by batch process.",
                        Open     = true,
                        Icon     = TextIcon.Note
                    };

                    // Add the annotation to the page's annotation collection
                    firstPage.Annotations.Add(annotation);

                    // Build output file name (original name + "_annotated.pdf")
                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(pdfPath) + "_annotated.pdf");

                    // Save the modified document
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Annotated: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation completed.");
    }
}