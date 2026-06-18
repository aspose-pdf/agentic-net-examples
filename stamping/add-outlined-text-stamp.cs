using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a single blank page
        using (Document document = new Document())
        {
            document.Pages.Add();
            document.Save("input.pdf");
        }

        // Open the sample PDF for stamping
        using (Document document = new Document("input.pdf"))
        {
            // Configure text state for outlined effect (simulated with a bold font)
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Arial-BoldMT");
            textState.FontSize = 48;
            textState.ForegroundColor = Color.Black;

            // Create a text stamp using the configured text state
            TextStamp textStamp = new TextStamp("Outlined Text", textState);
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Center;
            textStamp.Opacity = 1.0f;

            // Add the stamp to the first page (1‑based indexing)
            document.Pages[1].AddStamp(textStamp);

            // Save the stamped PDF
            document.Save("output.pdf");
        }
    }
}