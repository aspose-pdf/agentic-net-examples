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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Rotate page 3 by 180 degrees if it exists
                if (doc.Pages.Count >= 3)
                {
                    Page page3 = doc.Pages[3];
                    page3.Rotate = Rotation.on180;
                }

                // Rotate page 5 by 180 degrees if it exists
                if (doc.Pages.Count >= 5)
                {
                    Page page5 = doc.Pages[5];
                    page5.Rotate = Rotation.on180;
                }

                // Rotate page 7 by 180 degrees if it exists
                if (doc.Pages.Count >= 7)
                {
                    Page page7 = doc.Pages[7];
                    page7.Rotate = Rotation.on180;
                }

                doc.Save(outputPath);
                Console.WriteLine($"Pages rotated and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}