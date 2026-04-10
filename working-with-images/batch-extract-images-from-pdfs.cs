using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = @"C:\InputPdfs";
        // Folder where extracted images will be saved
        const string outputFolder = @"C:\ExtractedImages";

        // Verify that the input directory exists; if not, inform the user and exit gracefully.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder does not exist: '{inputFolder}'. Please create the folder and place PDF files inside before running the program.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Use a using block to guarantee deterministic disposal of the Document
                using (Document doc = new Document(pdfPath))
                {
                    // Create a subfolder for each PDF to avoid name collisions
                    string pdfName = Path.GetFileNameWithoutExtension(pdfPath);
                    string pdfOutputDir = Path.Combine(outputFolder, pdfName);
                    Directory.CreateDirectory(pdfOutputDir);

                    // Iterate over pages (Aspose.Pdf uses 1‑based indexing)
                    for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        int imageIndex = 1;
                        // Iterate over all images on the page using the correct collection enumeration
                        foreach (XImage img in page.Resources.Images)
                        {
                            // Determine a file name – most images can be saved as PNG safely
                            string imageFileName = $"page{pageIndex}_img{imageIndex}.png";
                            string imagePath = Path.Combine(pdfOutputDir, imageFileName);

                            // Save the image using the Stream overload (Aspose.Pdf expects a Stream)
                            using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                            {
                                img.Save(fs);
                            }

                            imageIndex++;
                        }
                    }
                }

                Console.WriteLine($"Extracted images from '{Path.GetFileName(pdfPath)}' to '{Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(pdfPath))}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process '{Path.GetFileName(pdfPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch image extraction completed.");
    }
}
