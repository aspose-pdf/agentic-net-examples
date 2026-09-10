using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = "InputPdfs";

        // Folder where extracted images will be saved
        const string outputFolder = "ExtractedImages";

        // Verify input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Load the PDF document (lifecycle rule: wrap Document in using)
                using (Document doc = new Document(pdfPath))
                {
                    string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

                    // Iterate pages (1‑based indexing)
                    for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
                    {
                        Page page = doc.Pages[pageIdx];
                        int imageIdx = 1;

                        // Iterate over images on the page (XImageCollection is not a dictionary)
                        foreach (XImage img in page.Resources.Images)
                        {
                            // Build a unique file name for each extracted image
                            string imageFileName = $"{pdfBaseName}_page{pageIdx}_img{imageIdx}.png";
                            string imagePath = Path.Combine(outputFolder, imageFileName);

                            // Save the image to the output folder using a stream overload (XImage.Save expects a Stream)
                            using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                            {
                                img.Save(fs);
                            }

                            Console.WriteLine($"Saved image: {imagePath}");
                            imageIdx++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors but continue processing remaining PDFs
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
