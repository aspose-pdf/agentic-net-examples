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
            // Get the document creation date (if not set, defaults to DateTime.MinValue)
            DateTime creationDate = doc.Info.CreationDate;
            string dateText = creationDate.ToString("yyyy-MM-dd");

            foreach (Page page in doc.Pages)
            {
                TextStamp stamp = new TextStamp(dateText);
                stamp.HorizontalAlignment = HorizontalAlignment.Left;
                stamp.VerticalAlignment   = VerticalAlignment.Top;
                // Use XIndent/YIndent instead of the non‑existent Margin property
                stamp.XIndent = 10; // distance from the left edge (points)
                stamp.YIndent = 10; // distance from the top edge (points)
                page.AddStamp(stamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
