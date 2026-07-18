using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based; iterate accordingly
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply a 90° rotation to pages whose number is divisible by 3
                if (i % 3 == 0)
                {
                    Page page = doc.Pages[i];
                    page.Rotate = Rotation.on90; // 90 degrees clockwise (use 'on' prefix)
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}