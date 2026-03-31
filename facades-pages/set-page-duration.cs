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

        using (Document document = new Document(inputPath))
        {
            // Verify the document has at least five pages
            if (document.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document has fewer than 5 pages.");
                return;
            }

            // Set the display duration of page 5 to five seconds
            Page pageFive = document.Pages[5];
            pageFive.Duration = 5.0;

            document.Save(outputPath);
        }

        Console.WriteLine($"Page 5 duration set to 5 seconds. Saved to '{outputPath}'.");
    }
}