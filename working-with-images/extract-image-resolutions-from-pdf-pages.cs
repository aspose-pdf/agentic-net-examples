using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    var res = placement.Resolution;
                    Console.WriteLine(
                        $"Page {i}: Image at ({placement.Rectangle.LLX}, {placement.Rectangle.LLY}) " +
                        $"size {placement.Rectangle.Width}x{placement.Rectangle.Height} " +
                        $"Resolution X={res.X} DPI, Y={res.Y} DPI");
                }
            }
        }
    }
}