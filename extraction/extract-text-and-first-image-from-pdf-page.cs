using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "page1.txt";
        const string outputImg = "page1_image1.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify at least one page exists (pages are 1‑based)
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("PDF contains no pages.");
                return;
            }

            // ----- Extract text from page 1 -----
            TextAbsorber absorber = new TextAbsorber();
            // Apply absorber to the first page
            doc.Pages[1].Accept(absorber);
            // Save extracted text to a .txt file
            File.WriteAllText(outputTxt, absorber.Text);

            // ----- Extract first image from page 1 -----
            Page firstPage = doc.Pages[1];
            // Iterate over the image resources of the page
            foreach (XImage img in firstPage.Resources.Images)
            {
                // Save the image to a file. XImage.Save expects a Stream, not a file path.
                using (FileStream fs = File.Create(outputImg))
                {
                    img.Save(fs);
                }
                // Only the first image is required
                break;
            }
        }

        Console.WriteLine($"Text extracted to: {outputTxt}");
        Console.WriteLine($"First image extracted to: {outputImg}");
    }
}
