using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddBottomLeftTextStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the sample PDF and add a bottom‑left text stamp with 10‑point margins
            using (Document doc = new Document("input.pdf"))
            {
                // Create the text stamp
                TextStamp textStamp = new TextStamp("Sample Stamp");
                textStamp.HorizontalAlignment = HorizontalAlignment.Left;
                textStamp.VerticalAlignment = VerticalAlignment.Bottom;
                textStamp.LeftMargin = 10;
                textStamp.BottomMargin = 10;

                // Apply the stamp to each page
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(textStamp);
                }

                // Save the updated PDF
                doc.Save("output.pdf");
            }
        }
    }
}
