using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bates numbering configuration (core API replacement for BatesNumberingOptions)
        string  prefix          = "ABC-";
        string  suffix          = "-2023";
        int     startNumber     = 1;
        int     numberOfDigits  = 5;
        double  margin          = 10;               // points – used for Right/Bottom margins
        HorizontalAlignment  hAlign = HorizontalAlignment.Right;
        VerticalAlignment    vAlign = VerticalAlignment.Bottom;
        int     fontSize        = 12;
        Aspose.Pdf.Color    fontColor = Aspose.Pdf.Color.Black;
        var     font           = FontRepository.FindFont("Helvetica");

        // Load the PDF document using the core Document class
        var doc = new Document(inputPath);

        // Add a TextStamp to each page representing the Bates number
        for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
        {
            int number = startNumber + pageIndex - 1;
            string numberString = number.ToString().PadLeft(numberOfDigits, '0');
            string stampText = $"{prefix}{numberString}{suffix}";

            var stamp = new TextStamp(stampText)
            {
                // Text appearance
                TextState =
                {
                    Font = font,
                    FontSize = fontSize,
                    ForegroundColor = fontColor
                },
                // Positioning – use core alignment properties and individual margins
                HorizontalAlignment = hAlign,
                VerticalAlignment   = vAlign,
                RightMargin = margin,
                BottomMargin = margin
            };

            doc.Pages[pageIndex].AddStamp(stamp);
        }

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Bates numbering added. Saved to '{outputPath}'.");
    }
}
