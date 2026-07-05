using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input folder containing PDF files
        const string inputFolder = @"C:\InputPdfs";
        // Output folder where extracted images will be saved
        const string outputFolder = @"C:\ExtractedImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input folder.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            // Use a using block for deterministic disposal of the Document (lifecycle rule)
            using (Document doc = new Document(pdfPath))
            {
                bool anyImageFound = false;
                int imageIndex = 1;

                // Iterate through each page and its image resources
                foreach (Page page in doc.Pages)
                {
                    // page.Resources.Images is a collection of XImage objects
                    foreach (XImage img in page.Resources.Images)
                    {
                        anyImageFound = true;

                        // Default to PNG – Aspose.Pdf.XImage.Save works with a Stream, the format is inferred from the file extension.
                        const string extension = "png";

                        string outputPath = Path.Combine(
                            outputFolder,
                            $"{Path.GetFileNameWithoutExtension(pdfPath)}_p{page.Number}_img{imageIndex}.{extension}");

                        // Save the image to disk via a FileStream (XImage.Save overload expects a Stream).
                        using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        Console.WriteLine($"Saved image: {outputPath}");
                        imageIndex++;
                    }
                }

                if (!anyImageFound)
                {
                    Console.WriteLine($"No images found in '{Path.GetFileName(pdfPath)}'.");
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
