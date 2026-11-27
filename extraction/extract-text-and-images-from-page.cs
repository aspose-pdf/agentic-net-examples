using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "page4.txt";
        const string imagesDir = "Page4Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output folder for images exists
        Directory.CreateDirectory(imagesDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the document has at least four pages (1‑based indexing)
                if (doc.Pages.Count < 4)
                {
                    Console.Error.WriteLine("The document contains fewer than 4 pages.");
                    return;
                }

                // Access page 4 (Aspose.Pdf uses 1‑based page indexing)
                Page page = doc.Pages[4];

                // -------------------------
                // Extract text from page 4
                // -------------------------
                TextAbsorber absorber = new TextAbsorber();
                page.Accept(absorber);                     // Apply absorber to the specific page
                File.WriteAllText(outputTxt, absorber.Text);
                Console.WriteLine($"Text from page 4 saved to '{outputTxt}'.");

                // ---------------------------------
                // Extract embedded images as JPEGs
                // ---------------------------------
                int imageIndex = 1;
                foreach (XImage img in page.Resources.Images)
                {
                    string imgPath = Path.Combine(imagesDir, $"image_{imageIndex}.jpg");

                    // XImage.Save works with a Stream, not a file path.
                    using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs); // Saves the image in its original format (JPEG for typical PDF images)
                    }

                    Console.WriteLine($"Image {imageIndex} saved to '{imgPath}'.");
                    imageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
