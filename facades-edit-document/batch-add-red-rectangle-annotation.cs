using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output directories (relative to the executable folder)
        const string inputDir  = "Input";
        const string outputDir = "Output";

        // Ensure both directories exist. If the Input folder is missing we create it
        // so that Directory.GetFiles does not throw a DirectoryNotFoundException.
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in the '{inputDir}' folder.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            try
            {
                // Determine output file path (same name, different folder)
                string fileName   = System.IO.Path.GetFileName(inputPath);
                string outputPath = System.IO.Path.Combine(outputDir, fileName);

                // Use PdfAnnotationEditor (facade) to open the PDF
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(inputPath);               // Load the PDF

                    // Access the underlying Document
                    Document doc = editor.Document;

                    // Loop through all pages (1‑based indexing)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        // Define rectangle coordinates (llx, lly, urx, ury)
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                        // Create a square (rectangle) annotation with red border
                        SquareAnnotation square = new SquareAnnotation(doc.Pages[pageNum], rect)
                        {
                            Color = Aspose.Pdf.Color.Red   // Border color
                        };

                        // Add the annotation to the page
                        doc.Pages[pageNum].Annotations.Add(square);
                    }

                    // Save the modified PDF
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Processed: {fileName} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch annotation completed.");
    }
}
