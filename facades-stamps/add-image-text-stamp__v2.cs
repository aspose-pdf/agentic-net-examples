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
        const string logoPath = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {logoPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Image stamp for the company logo
            ImageStamp imgStamp = new ImageStamp(logoPath);
            imgStamp.Background = true;
            imgStamp.Opacity = 0.5f;
            imgStamp.XIndent = 50;   // horizontal position
            imgStamp.YIndent = 750;  // vertical position (from bottom)
            imgStamp.Width = 100;
            imgStamp.Height = 50;

            // Text stamp for the word "Confidential" in bold
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Helvetica-Bold");
            textState.FontSize = 24;
            textState.ForegroundColor = Aspose.Pdf.Color.Red;
            TextStamp txtStamp = new TextStamp("Confidential", textState);
            txtStamp.Background = true;
            txtStamp.Opacity = 0.5f;
            txtStamp.XIndent = 200;   // horizontal position
            txtStamp.YIndent = 750;   // vertical position (from bottom)
            txtStamp.Width = 200;
            txtStamp.Height = 50;
            txtStamp.TextAlignment = HorizontalAlignment.Center;

            // Apply both stamps to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
                page.AddStamp(txtStamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
