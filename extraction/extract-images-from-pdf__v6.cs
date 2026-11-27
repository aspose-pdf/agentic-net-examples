using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];
                int imageIndex = 1;

                // Iterate over images; XImageCollection is not a dictionary
                foreach (XImage img in page.Resources.Images)
                {
                    // Build filename with page and image indices prefixed
                    string fileName = $"{pageNumber:D3}_{imageIndex:D3}.png";
                    string outPath = Path.Combine(outputDir, fileName);

                    // Save the image to a file via a FileStream (XImage.Save expects a Stream)
                    using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved image: {outPath}");
                    imageIndex++;
                }
            }
        }
    }
}
