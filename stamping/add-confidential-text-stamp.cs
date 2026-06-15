using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddTextStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF
            using (Document document = new Document())
            {
                Page page = document.Pages.Add();
                TextFragment fragment = new TextFragment("Sample PDF content.");
                page.Paragraphs.Add(fragment);
                document.Save("input.pdf");
            }

            // Open the PDF and add a semi‑transparent text stamp
            using (Document document = new Document("input.pdf"))
            {
                TextStamp textStamp = new TextStamp("Confidential");
                textStamp.Opacity = 0.6;
                textStamp.HorizontalAlignment = HorizontalAlignment.Center;
                textStamp.VerticalAlignment = VerticalAlignment.Center;
                foreach (Page page in document.Pages)
                {
                    page.AddStamp(textStamp);
                }
                document.Save("output.pdf");
            }
        }
    }
}
