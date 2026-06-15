using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Simple opacity calculation based on page count
    static double GetOpacity(int pageCount)
    {
        if (pageCount <= 10) return 0.3;
        if (pageCount <= 50) return 0.5;
        return 0.7;
    }

    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where watermarked PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                using (Document doc = new Document(inputPath))
                {
                    int pageCount = doc.Pages.Count;
                    double opacity = GetOpacity(pageCount);

                    // Add a watermark artifact to every page
                    foreach (Page page in doc.Pages)
                    {
                        WatermarkArtifact watermark = new WatermarkArtifact
                        {
                            IsBackground = true,
                            Opacity = opacity,
                            Text = "CONFIDENTIAL",
                            ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                            ArtifactVerticalAlignment = VerticalAlignment.Center,
                            TextState = new TextState
                            {
                                Font = FontRepository.FindFont("Helvetica"),
                                FontSize = 72,
                                ForegroundColor = Aspose.Pdf.Color.Red
                            }
                        };

                        page.Artifacts.Add(watermark);
                    }

                    string outputPath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(inputPath) + "_watermarked.pdf");

                    doc.Save(outputPath);
                    Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch watermarking completed.");
    }
}