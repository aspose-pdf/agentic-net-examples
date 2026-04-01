using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            if (pageCount == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Get the last page (1‑based indexing)
            Page lastPage = doc.Pages[pageCount];

            // Prepare the date string in MM-dd-yyyy format
            string dateString = DateTime.Now.ToString("MM-dd-yyyy");

            // Create a text stamp with the date
            TextStamp dateStamp = new TextStamp(dateString);
            dateStamp.HorizontalAlignment = HorizontalAlignment.Center;
            dateStamp.VerticalAlignment = VerticalAlignment.Bottom;
            dateStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            dateStamp.TextState.FontSize = 12;
            dateStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            // Optional: set a small bottom margin (float literal)
            dateStamp.BottomMargin = 10f;

            // Add the stamp only to the last page
            lastPage.AddStamp(dateStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer with current date added to last page. Saved as '{outputPath}'.");
    }
}
