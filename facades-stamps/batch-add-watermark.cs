using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input folder containing PDF files
        string inputFolder = "input-pdfs";
        Directory.CreateDirectory(inputFolder);

        // Process each PDF file in the folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFileName = fileNameWithoutExt + "_wm.pdf"; // simple filename, no directory path

            using (Document document = new Document(pdfPath))
            {
                // Add the same multi‑line watermark to every page
                foreach (Page page in document.Pages)
                {
                    WatermarkArtifact watermark = new WatermarkArtifact();
                    watermark.SetTextAndState(
                        "CONFIDENTIAL\nDo Not Distribute",
                        new TextState
                        {
                            FontSize = 48,
                            ForegroundColor = Color.FromArgb(50, 255, 0, 0) // semi‑transparent red
                        }
                    );
                    watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                    watermark.ArtifactVerticalAlignment = VerticalAlignment.Center;
                    watermark.Rotation = 45;
                    watermark.Opacity = 0.5f;
                    watermark.IsBackground = true;
                    page.Artifacts.Add(watermark);
                }

                document.Save(outputFileName);
                Console.WriteLine($"Watermarked PDF saved as '{outputFileName}'.");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
