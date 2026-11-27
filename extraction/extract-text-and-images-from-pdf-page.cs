using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdfPath = "input.pdf";

        // Output paths
        const string outputTextPath = "page2_text.txt";
        const string imagesOutputDir = "Page2_Images";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; page 2 is at index 2
            if (pdfDoc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document has fewer than 2 pages.");
                return;
            }

            Page pageTwo = pdfDoc.Pages[2];

            // ---------- Extract text from page 2 ----------
            TextAbsorber absorber = new TextAbsorber();
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            pageTwo.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Save extracted text to a .txt file
            File.WriteAllText(outputTextPath, extractedText);
            Console.WriteLine($"Text from page 2 saved to '{outputTextPath}'.");

            // ---------- Extract images from page 2 ----------
            int imageIndex = 1;
            foreach (XImage img in pageTwo.Resources.Images)
            {
                string imagePath = Path.Combine(imagesOutputDir, $"page2_image_{imageIndex}.jpg");
                // XImage.Save expects a Stream, not a file path.
                using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    img.Save(fs);
                }
                Console.WriteLine($"Image {imageIndex} saved to '{imagePath}'.");
                imageIndex++;
            }

            if (imageIndex == 1)
                Console.WriteLine("No images found on page 2.");
        }
    }
}
