using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class BatchAddTextAnnotation
{
    static void Main()
    {
        // Determine base folder (current executable directory) – works on any OS
        string baseFolder = AppDomain.CurrentDomain.BaseDirectory;

        // Input folder – "PdfFolder" under the base directory
        string inputFolder = Path.Combine(baseFolder, "PdfFolder");
        // Output folder – "PdfFolder\Processed" under the base directory
        string outputFolder = Path.Combine(baseFolder, "PdfFolder", "Processed");

        // Ensure both folders exist (create if missing). If the input folder is empty, the program will simply finish.
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Open each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Access the first page (1‑based indexing)
                Page firstPage = doc.Pages[1];

                // Define the annotation rectangle (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a TextAnnotation and set its properties
                TextAnnotation annotation = new TextAnnotation(firstPage, rect)
                {
                    Title    = "Note",
                    Contents = "Standard annotation added by batch process.",
                    Open     = true,
                    // Use Aspose.Pdf.Color for cross‑platform compatibility
                    Color    = Aspose.Pdf.Color.Yellow,
                    // Icon can be set to any supported value, e.g., Note
                    Icon     = TextIcon.Note
                };

                // Add the annotation to the page's annotation collection
                firstPage.Annotations.Add(annotation);

                // Build the output file path (same file name, different folder)
                string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                // Save the modified PDF – the same call works on Windows and non‑Windows platforms.
                // If GDI+ (libgdiplus) is missing on Linux/macOS, Aspose.Pdf will still save the file; the try/catch is kept for safety.
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine($"Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering‑dependent features: {outputPath}");
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
        }

        Console.WriteLine("Batch processing completed.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
