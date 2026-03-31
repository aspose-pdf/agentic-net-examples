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
            if (document.Pages.Count >= 6)
            {
                // Reset rotation of page 6 to 0 degrees
                document.Pages[6].Rotate = Rotation.None;
                document.Save(outputPath);
                Console.WriteLine($"Page 6 rotation reset. Saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine("The document contains fewer than 6 pages.");
            }
        }
    }
}