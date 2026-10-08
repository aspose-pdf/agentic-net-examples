using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Build absolute, platform‑agnostic paths based on the executable location.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "ExtractedImages");

        // Ensure the folders exist – create the output folder, and fall back to the base directory
        // if the input folder is missing (so the sample can run out‑of‑the‑box).
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' not found. Using base directory as fallback.");
            inputFolder = baseDir; // fallback to current directory
        }
        Directory.CreateDirectory(outputFolder);

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
                // Use the high‑level Document API to obtain the page count.
                using (Document doc = new Document(pdfPath))
                {
                    int pageCount = doc.Pages.Count;
                    string pdfBaseName = Path.GetFileNameWithoutExtension(pdfPath);

                    for (int page = 1; page <= pageCount; page++)
                    {
                        using (PdfExtractor extractor = new PdfExtractor())
                        {
                            extractor.BindPdf(pdfPath);
                            extractor.StartPage = page;
                            extractor.EndPage = page;
                            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
                            extractor.ExtractImage();

                            int imageIndex = 1;
                            while (extractor.HasNextImage())
                            {
                                using (MemoryStream imgStream = new MemoryStream())
                                {
                                    extractor.GetNextImage(imgStream);
                                    imgStream.Position = 0;

                                    string outputFile = Path.Combine(
                                        outputFolder,
                                        $"{pdfBaseName}_page{page}_img{imageIndex}.png");

                                    File.WriteAllBytes(outputFile, imgStream.ToArray());
                                }
                                imageIndex++;
                            }
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
