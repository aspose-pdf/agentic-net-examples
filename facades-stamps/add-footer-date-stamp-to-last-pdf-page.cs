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
            // Determine the last page number
            int lastPage = doc.Pages.Count;

            // Create a text stamp with the current date (MM-dd-yyyy)
            string dateText = DateTime.Now.ToString("MM-dd-yyyy");
            TextStamp dateStamp = new TextStamp(dateText)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                BottomMargin        = 10,
                Background          = false
            };

            // TextState is read‑only – modify the existing instance instead of assigning a new one
            dateStamp.TextState.FontSize = 12;
            dateStamp.TextState.ForegroundColor = Color.Black;

            // Apply the stamp only to the last page
            doc.Pages[lastPage].AddStamp(dateStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer date stamp added to page {new Document(inputPath).Pages.Count}. Saved as '{outputPath}'.");
    }
}
