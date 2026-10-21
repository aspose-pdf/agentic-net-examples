using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using Document (lifecycle managed via using)
        using (Document doc = new Document(inputPath))
        {
            // Determine the last page (1‑based indexing)
            int lastPageNumber = doc.Pages.Count;

            // Create a TextStamp with the current date formatted as MM-dd-yyyy
            string dateText = DateTime.Now.ToString("MM-dd-yyyy");
            TextStamp dateStamp = new TextStamp(dateText)
            {
                BottomMargin = 10,                     // distance from bottom edge
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // TextStamp.TextState is read‑only; modify its members directly
            dateStamp.TextState.FontSize = 12;
            dateStamp.TextState.FontStyle = FontStyles.Bold;
            // Optional styling (uncomment if needed)
            // dateStamp.TextState.Font = FontRepository.FindFont("Arial");
            // dateStamp.TextState.ForegroundColor = Color.Black;

            // Apply the stamp only to the last page
            Page lastPage = doc.Pages[lastPageNumber];
            lastPage.AddStamp(dateStamp);

            // Save the modified document
            doc.Save(outputPath);

            Console.WriteLine($"Footer stamp added to page {lastPageNumber}. Saved as '{outputPath}'.");
        }
    }
}
