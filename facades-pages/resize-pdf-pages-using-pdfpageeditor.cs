using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Load PDF bytes from the (now guaranteed) file.
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Create a MemoryStream from the byte array and load it into a Document.
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            // Define the new page size (width and height in points).
            // Example: A5 size (420 x 595 points).
            const double newWidth = 420;   // points
            const double newHeight = 595;  // points

            // Apply the new size to every page via the PageInfo object.
            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width = newWidth;
                page.PageInfo.Height = newHeight;
                // Optional: set orientation flag based on dimensions.
                page.PageInfo.IsLandscape = newWidth > newHeight;
            }

            // Save the modified PDF to a new file.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page size modified and saved to '{outputPath}'.");
    }
}
