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

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count >= 3)
            {
                // Rotate page 3 by 90 degrees and set its size to Letter (8.5" x 11" = 612 x 792 points)
                Page page = doc.Pages[3];
                page.Rotate = Rotation.on90;
                page.SetPageSize(612, 792);
            }
            else
            {
                Console.Error.WriteLine("Document does not have a third page.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
