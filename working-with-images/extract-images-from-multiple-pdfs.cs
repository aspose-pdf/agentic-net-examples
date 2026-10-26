using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Base directory of the application (works for both Windows and Linux)
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // Resolve input and output folders relative to the base directory
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "ExtractedImages");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Validate (or create) the input directory – if it does not exist we create it
        // and inform the user that there are no PDFs to process.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Input folder not found. Created empty folder at: {inputFolder}");
            Console.WriteLine("Place PDF files in the input folder and run the program again.");
            return;
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputFolder}'." );
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                    for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                    {
                        Page page = doc.Pages[pageNumber];
                        int imageIndex = 0;

                        // Iterate over images on the page
                        foreach (XImage img in page.Resources.Images)
                        {
                            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                            string fileName = $"{baseName}_page{pageNumber}_img{imageIndex}.png";
                            string outputPath = Path.Combine(outputFolder, fileName);

                            // Save the image to the output folder
                            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                            {
                                img.Save(fs);
                            }

                            imageIndex++;
                        }
                    }
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{Path.GetFileName(pdfPath)}': {ex.Message}");
                // Continue with the next file instead of aborting the whole batch
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
