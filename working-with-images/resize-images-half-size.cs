using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // The page may contain Image objects (Aspose.Pdf.Image) in its Paragraphs collection.
                // These objects expose the ImageScale property which can be used to resize the image.
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Image imgObj)
                    {
                        // Scale the image to 50% of its original size.
                        imgObj.ImageScale = 0.5f;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
