using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The document has fewer than 10 pages.");
                return;
            }

            TextStamp stamp = new TextStamp("Sample Text");
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.TextState.Underline = true;
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 24;
            stamp.TextState.ForegroundColor = Color.Black;
            stamp.TextState.BackgroundColor = Color.Yellow;

            Page page = doc.Pages[10];
            page.AddStamp(stamp);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp added and saved to '{outputPath}'.");
    }
}
