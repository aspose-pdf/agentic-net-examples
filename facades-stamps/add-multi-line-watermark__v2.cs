using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // First line – largest font size
            TextState textState1 = new TextState();
            textState1.Font = FontRepository.FindFont("Arial");
            textState1.FontSize = 48;
            textState1.ForegroundColor = Aspose.Pdf.Color.Gray;

            TextStamp stamp1 = new TextStamp("CONFIDENTIAL", textState1);
            stamp1.Opacity = 0.5f;
            stamp1.HorizontalAlignment = HorizontalAlignment.Center;
            stamp1.VerticalAlignment = VerticalAlignment.Center;
            stamp1.YIndent = 400; // position from bottom of page

            // Second line – medium font size
            TextState textState2 = new TextState();
            textState2.Font = FontRepository.FindFont("Arial");
            textState2.FontSize = 36;
            textState2.ForegroundColor = Aspose.Pdf.Color.Gray;

            TextStamp stamp2 = new TextStamp("Do Not Distribute", textState2);
            stamp2.Opacity = 0.5f;
            stamp2.HorizontalAlignment = HorizontalAlignment.Center;
            stamp2.VerticalAlignment = VerticalAlignment.Center;
            stamp2.YIndent = 350;

            // Third line – smallest font size
            TextState textState3 = new TextState();
            textState3.Font = FontRepository.FindFont("Arial");
            textState3.FontSize = 24;
            textState3.ForegroundColor = Aspose.Pdf.Color.Gray;

            TextStamp stamp3 = new TextStamp("Company Name", textState3);
            stamp3.Opacity = 0.5f;
            stamp3.HorizontalAlignment = HorizontalAlignment.Center;
            stamp3.VerticalAlignment = VerticalAlignment.Center;
            stamp3.YIndent = 300;

            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp1);
                page.AddStamp(stamp2);
                page.AddStamp(stamp3);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine("Watermarked PDF saved to '" + outputPath + "'.");
    }
}