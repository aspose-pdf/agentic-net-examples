using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // required for Image class

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages in the document
            foreach (Page page in doc.Pages)
            {
                // Iterate through each placed image on the current page
                foreach (var paragraph in page.Paragraphs)
                {
                    if (paragraph is Image img)
                    {
                        // Resize the image by setting its FixWidth and FixHeight (points)
                        img.FixWidth = 200;   // desired width
                        img.FixHeight = 150;  // desired height
                    }
                }
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized images saved to '{outputPath}'.");
    }
}
