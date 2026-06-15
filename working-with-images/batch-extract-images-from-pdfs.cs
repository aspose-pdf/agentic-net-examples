using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input folder containing PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where extracted images will be saved
        const string outputFolder = @"C:\ExtractedImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document doc = new Document(pdfPath))
                {
                    string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

                    // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                    {
                        Page page = doc.Pages[pageNum];
                        int imageIndex = 1;

                        // Iterate over the XImage collection (direct foreach, not a dictionary)
                        foreach (XImage img in page.Resources.Images)
                        {
                            // Build a unique file name for each extracted image
                            string imageFileName = $"{pdfBaseName}_page{pageNum}_img{imageIndex}.png";
                            string imagePath = Path.Combine(outputFolder, imageFileName);

                            // Save the image to the output folder using a FileStream because the
                            // XImage.Save overload that accepts a string path is not available in
                            // the referenced Aspose.Pdf version.
                            using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                            {
                                img.Save(fs);
                            }

                            Console.WriteLine($"Extracted: {imagePath}");
                            imageIndex++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
