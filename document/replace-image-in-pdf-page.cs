using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Image class lives here

class ReplaceImageInPdf
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputPdfPath = "output.pdf";  // result PDF
        const string newImagePath  = "newImage.jpg"; // replacement image

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        // Load the whole PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing – replace images on the first page
            Page page = doc.Pages[1];

            // Read the replacement image once – reuse the byte array for every occurrence
            byte[] newImageBytes = File.ReadAllBytes(newImagePath);

            // Walk through all paragraph elements on the page and replace any Image element
            foreach (var paragraph in page.Paragraphs)
            {
                if (paragraph is Image img)
                {
                    // Preserve layout (position, size, margins) – only swap the underlying image data
                    img.ImageStream = new MemoryStream(newImageBytes);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image(s) replaced and PDF saved to '{outputPdfPath}'.");
    }
}
