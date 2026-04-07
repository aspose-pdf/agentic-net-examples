using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string base64Image = "<BASE64_STRING>";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            ImageStamp stamp = new ImageStamp(imageStream);
            double mmToPoints = 2.834645669;
            stamp.XIndent = 50.0 * mmToPoints;
            stamp.YIndent = 50.0 * mmToPoints;

            using (Document doc = new Document(inputPath))
            {
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("Document does not have a third page.");
                    return;
                }

                Page page = doc.Pages[3];
                page.AddStamp(stamp);
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}