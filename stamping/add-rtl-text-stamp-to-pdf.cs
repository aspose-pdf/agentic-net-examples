using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_rtl.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Arabic (or Hebrew) text to be stamped
        const string rtlText = "مثال على نص عربي من اليمين إلى اليسار";

        // Create a TextStamp with the RTL text
        TextStamp stamp = new TextStamp(rtlText);

        // Configure the visual appearance of the stamp using the existing TextState instance
        TextState ts = stamp.TextState;
        ts.Font = FontRepository.FindFont("Arial");
        ts.FontSize = 24;
        ts.ForegroundColor = Color.Blue;
        // If the used Aspose.PDF version supports RTL, the following line can be uncommented:
        // ts.IsRightToLeft = true; // property may not exist in older versions

        // Position the stamp at the centre of each page
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment   = VerticalAlignment.Center;

        // Optional: make the stamp semi‑transparent
        stamp.Opacity = 0.7f;

        // Apply the stamp to every page in the document
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"RTL text stamp applied and saved to '{outputPath}'.");
    }
}
