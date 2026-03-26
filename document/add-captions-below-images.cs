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
            int pageCount = doc.Pages.Count;
            for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                int imageIndex = 0;
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;
                    // Example caption rectangle positioned near the bottom of the page
                    double captionLeft = 50.0;
                    double captionBottom = 50.0 + (imageIndex - 1) * 30.0;
                    double captionRight = page.PageInfo.Width - 50.0;
                    double captionTop = captionBottom + 20.0;

                    TextParagraph caption = new TextParagraph();
                    caption.Rectangle = new Aspose.Pdf.Rectangle(captionLeft, captionBottom, captionRight, captionTop);
                    caption.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;
                    caption.HorizontalAlignment = HorizontalAlignment.Center;

                    string captionText = $"Image {imageIndex} caption";
                    caption.AppendLine(captionText);

                    TextBuilder builder = new TextBuilder(page);
                    builder.AppendParagraph(caption);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Captions added and saved to '{outputPath}'.");
    }
}