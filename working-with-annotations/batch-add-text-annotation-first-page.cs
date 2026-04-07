using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs (relative to the executable directory)
        const string inputFolder = "InputPdfs";
        // Folder where annotated PDFs will be written
        const string outputFolder = "OutputPdfs";

        // Resolve paths relative to the current working directory for robustness
        string inputPath = Path.GetFullPath(inputFolder);
        string outputPath = Path.GetFullPath(outputFolder);

        // Verify the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input folder '{inputPath}' does not exist – nothing to process.");
            return; // Gracefully exit the program
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputPath);

        // Retrieve all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputPath, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Build the output file path (same file name, different folder)
                string outFile = Path.Combine(outputPath, Path.GetFileName(pdfPath));

                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Verify the document has at least one page
                    if (doc.Pages.Count < 1)
                    {
                        Console.Error.WriteLine($"File '{pdfPath}' contains no pages – skipping.");
                        continue;
                    }

                    // Get the first page (Aspose.Pdf uses 1‑based indexing)
                    Page firstPage = doc.Pages[1];

                    // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    // Create a TextAnnotation and configure its properties
                    TextAnnotation annotation = new TextAnnotation(firstPage, rect)
                    {
                        Title    = "Standard Note",
                        Contents = "This is a standard annotation added to the first page.",
                        Open     = true,                     // annotation window opened by default
                        Icon     = TextIcon.Note,            // note‑style icon
                        Color    = Aspose.Pdf.Color.Yellow   // background color of the annotation
                    };

                    // Add the annotation to the page's annotation collection
                    firstPage.Annotations.Add(annotation);

                    // Save the modified document to the designated output path
                    doc.Save(outFile);
                }

                Console.WriteLine($"Annotated '{pdfPath}' → '{outFile}'.");
            }
            catch (Exception ex)
            {
                // Log the error but continue processing the remaining files
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
