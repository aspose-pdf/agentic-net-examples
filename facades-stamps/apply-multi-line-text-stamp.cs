using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Create formatted text with custom line spacing (5.0 points between lines)
        FormattedText formattedText = new FormattedText("CONFIDENTIAL");
        formattedText.AddNewLineText("DO NOT DISTRIBUTE", 5.0f);
        formattedText.AddNewLineText("FOR INTERNAL USE ONLY", 5.0f);

        // Create a text stamp from the formatted text
        TextStamp stamp = new TextStamp(formattedText);
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment = VerticalAlignment.Center;
        stamp.Opacity = 0.5f;
        stamp.Background = false;

        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine("Stamped PDF saved to '" + outputPath + "'.");
    }
}