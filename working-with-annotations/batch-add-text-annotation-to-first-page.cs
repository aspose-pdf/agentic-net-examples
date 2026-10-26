using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        string inputFolder = "InputPdfs";
        // Folder where annotated PDFs will be saved
        string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count < 1)
                    {
                        Console.WriteLine($"Skipping empty PDF: {Path.GetFileName(pdfPath)}");
                        continue;
                    }

                    // First page (Aspose.Pdf uses 1‑based indexing)
                    Page firstPage = doc.Pages[1];

                    // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    // Create a text annotation and set its properties
                    TextAnnotation annotation = new TextAnnotation(firstPage, rect)
                    {
                        Title    = "Note",
                        Contents = "Standard annotation added by batch process.",
                        Open     = true,
                        Icon     = TextIcon.Note
                    };

                    // Attach the annotation to the first page
                    firstPage.Annotations.Add(annotation);

                    // Save the modified PDF to the output folder, preserving the original name
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation completed.");
    }
}