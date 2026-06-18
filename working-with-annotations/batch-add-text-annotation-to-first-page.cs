using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAddTextAnnotation
{
    static void Main()
    {
        // Folder containing PDF files to process
        const string inputFolder = @"C:\PdfInput";
        // Folder where processed PDFs will be saved (can be the same as inputFolder to overwrite)
        const string outputFolder = @"C:\PdfOutput";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Define the annotation that will be added to each first page
        // Position: lower‑left (100, 500), upper‑right (300, 550) – adjust as needed
        Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
        const string annotTitle = "Standard Note";
        const string annotContents = "This is a standard text annotation added to the first page.";
        Aspose.Pdf.Color annotColor = Aspose.Pdf.Color.Yellow; // Background color of the annotation

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count < 1)
                    {
                        Console.WriteLine($"Skipping '{fileName}': no pages found.");
                        continue;
                    }

                    // Create a new TextAnnotation on the first page
                    Page firstPage = doc.Pages[1]; // 1‑based indexing
                    TextAnnotation textAnnot = new TextAnnotation(firstPage, annotRect)
                    {
                        Title = annotTitle,
                        Contents = annotContents,
                        Color = annotColor,
                        Open = true // annotation window opened by default
                    };

                    // Add the annotation to the page's annotation collection
                    firstPage.Annotations.Add(textAnnot);

                    // Save the modified document (overwrites if outputPath equals inputPath)
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed and saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation completed.");
    }
}