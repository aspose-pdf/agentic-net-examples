using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 8 pages
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("Document does not contain page 8.");
                return;
            }

            // Store the original dimensions of page 8 (width and height in points)
            double originalWidth  = doc.Pages[8].PageInfo.Width;
            double originalHeight = doc.Pages[8].PageInfo.Height;

            // Example modification: change page 8 size to A4 (595 x 842 points)
            doc.Pages[8].PageInfo.Width  = 595;
            doc.Pages[8].PageInfo.Height = 842;

            // ... any other operations could be performed here ...

            // Revert page 8 back to its original dimensions using the stored values
            doc.Pages[8].PageInfo.Width  = originalWidth;
            doc.Pages[8].PageInfo.Height = originalHeight;

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 8 size reverted and saved to '{outputPath}'.");
    }
}
