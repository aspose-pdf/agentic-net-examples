using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        int imageIndex = 1;

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (Aspose.Pdf)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate the XImage collection directly (not as a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a filename that includes the extraction order number
                    string fileName = System.IO.Path.Combine(outputDir, $"image_{imageIndex}.png");

                    // Save the image to disk using a FileStream (XImage.Save expects a Stream)
                    using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved {fileName}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
