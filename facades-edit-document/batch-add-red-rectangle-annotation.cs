using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Base directory of the executable (works on any platform)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "Input");
        string outputFolder = Path.Combine(baseDir, "Output");

        // Ensure both folders exist – create them if they are missing
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (if any)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'. Place PDFs there and rerun the program.");
            return;
        }

        foreach (string inputPath in pdfFiles)
        {
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Open the PDF document
                using (Document doc = new Document(inputPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count > 0)
                    {
                        // Get the first page (1‑based indexing)
                        Page page = doc.Pages[1];

                        // Define the rectangle area (llx, lly, urx, ury)
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                        // Create a square (rectangle) annotation with a red border
                        SquareAnnotation square = new SquareAnnotation(page, rect)
                        {
                            Color = Aspose.Pdf.Color.Red // Border color
                            // FillColor = Aspose.Pdf.Color.Transparent // optional transparent fill
                        };

                        // Add the annotation to the page
                        page.Annotations.Add(square);
                    }

                    // Save the modified PDF to the output location
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Annotated PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}
