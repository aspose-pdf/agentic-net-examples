using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_separator.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a new empty page at the end of the document
            Page separatorPage = doc.Pages.Add();

            // Aspose.Pdf versions prior to certain releases do not expose a BackgroundColor
            // property on PageInfo. The default page background is effectively transparent for
            // most PDF viewers, so we simply keep the page empty. If a specific background
            // color is required, it can be simulated with a rectangle stamp.

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Separator page added and saved to '{outputPath}'.");
    }
}
