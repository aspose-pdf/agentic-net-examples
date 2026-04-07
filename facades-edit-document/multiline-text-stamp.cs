using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define text state with Arial, 14pt, blue color
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Arial");
            textState.FontSize = 14;
            textState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Multiline stamp content
            string stampText = "First line\nSecond line\nThird line";

            // Create the text stamp
            TextStamp stamp = new TextStamp(stampText, textState);
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;

            // Add stamp to the page
            page.AddStamp(stamp);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}