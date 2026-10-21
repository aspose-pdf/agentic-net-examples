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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Determine the creation date (fallback to now if not set)
            DateTime creationDate = doc.Info.CreationDate != DateTime.MinValue
                ? doc.Info.CreationDate
                : DateTime.Now;
            string dateText = creationDate.ToString("yyyy-MM-dd");

            // Create a text stamp with the formatted date
            TextStamp stamp = new TextStamp(dateText)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                XIndent = 10, // left margin
                YIndent = 10, // top margin
                TextState = {
                    Font = FontRepository.FindFont("Arial"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };

            // Apply the stamp to every page of the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the stamped document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
