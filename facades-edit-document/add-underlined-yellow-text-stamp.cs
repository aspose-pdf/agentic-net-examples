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
        const string stampText = "Confidential";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("Document has less than 10 pages.");
                return;
            }

            TextStamp textStamp = new TextStamp(stampText);
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Center;
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.FontSize = 48;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            textStamp.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;
            textStamp.TextState.Underline = true;

            Page pageTen = doc.Pages[10];
            pageTen.AddStamp(textStamp);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added to page 10 and saved as '{outputPath}'.");
    }
}
