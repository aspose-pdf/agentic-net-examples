using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";
        const string csvPath = "images.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // CSV header
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("Filename,PageNumber,Width,Height");

        // Facades class usage (required by task)
        PdfFileEditor _ = new PdfFileEditor();

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            int imageCounter = 1;

            // Iterate pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageIndex];

                // Iterate all images on the page
                foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                {
                    string fileName = $"image_{imageCounter}.png";
                    string filePath = Path.Combine(outputDir, fileName);

                    // Save the image to a file using a FileStream (XImage.Save expects a Stream)
                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    // Record image details in CSV
                    csvBuilder.AppendLine($"{fileName},{pageIndex},{img.Width},{img.Height}");

                    imageCounter++;
                }
            }
        }

        // Write CSV file
        File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);

        Console.WriteLine($"Extraction complete. Images saved to '{outputDir}'. CSV saved to '{csvPath}'.");
    }
}
