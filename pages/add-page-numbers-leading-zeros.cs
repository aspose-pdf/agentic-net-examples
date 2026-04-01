using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF with a few pages.
        // NOTE: In evaluation mode Aspose.PDF allows a maximum of 4 pages.
        // If you have a full license you can increase the count as needed.
        using (Document doc = new Document())
        {
            // Add up to 4 pages to stay within the evaluation limit.
            for (int i = 0; i < 4; i++)
            {
                doc.Pages.Add();
            }

            // Insert page numbers with leading zeros (e.g., 01, 02, ...)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                string paddedNumber = pageIndex.ToString("D2"); // two‑digit format

                TextStamp stamp = new TextStamp(paddedNumber)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    BottomMargin = 10f,
                    TextState = { FontSize = 12 }
                };

                page.AddStamp(stamp);
            }

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}
