using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

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

        // Create formatted text with initial line using System.Drawing.Color (matches the Facades overload)
        FormattedText formattedText = new FormattedText(
            "Confidential",
            System.Drawing.Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            36f);

        // Add additional lines with a line spacing of 1.5 (float)
        formattedText.AddNewLineText("Do Not Distribute", 1.5f);
        formattedText.AddNewLineText("Authorized Personnel Only", 1.5f);

        // Create a text stamp from the formatted text
        TextStamp textStamp = new TextStamp(formattedText);
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;
        textStamp.VerticalAlignment = VerticalAlignment.Center;
        textStamp.Opacity = 0.5f;
        textStamp.Background = false; // draw on top of page content

        using (Document document = new Document(inputPath))
        {
            foreach (Page page in document.Pages)
            {
                page.AddStamp(textStamp);
            }

            document.Save(outputPath);
        }

        Console.WriteLine("Text stamp applied and saved to '" + outputPath + "'.");
    }
}
