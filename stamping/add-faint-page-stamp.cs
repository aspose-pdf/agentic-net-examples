using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF with three pages (evaluation mode limit)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the PDF and add a semi‑transparent text stamp to each page
        using (Document pdfDoc = new Document("input.pdf"))
        {
            TextStamp stamp = new TextStamp("CONFIDENTIAL");
            stamp.Opacity = 0.4f;
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment = VerticalAlignment.Center;
            stamp.TextState.FontSize = 72;
            stamp.TextState.FontStyle = FontStyles.Bold;

            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                pdfDoc.Pages[pageNumber].AddStamp(stamp);
            }

            pdfDoc.Save("output.pdf");
        }
    }
}