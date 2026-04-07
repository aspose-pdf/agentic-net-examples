using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Determine the centre of page eight
            using (Document doc = new Document(inputPath))
            {
                if (doc.Pages.Count < 8)
                {
                    Console.Error.WriteLine("The document has fewer than 8 pages.");
                    return;
                }

                Page page = doc.Pages[8];
                double centreX = page.PageInfo.Width / 2.0;
                double centreY = page.PageInfo.Height / 2.0;

                // Move the first stamp on page eight to the centre
                editor.MoveStamp(8, 1, centreX, centreY);
            }

            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPath}'.");
    }
}
