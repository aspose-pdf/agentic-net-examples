using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains at least seven pages (1‑based indexing)
            if (doc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The document has fewer than 7 pages.");
                return;
            }

            // Retrieve page 7
            Page page7 = doc.Pages[7];

            // Obtain the predefined Letter page size
            var letterSize = Aspose.Pdf.PageSize.PageLetter;

            // Apply the Letter size to page 7
            page7.SetPageSize(letterSize.Width, letterSize.Height);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 7 resized to Letter format and saved as '{outputPath}'.");
    }
}