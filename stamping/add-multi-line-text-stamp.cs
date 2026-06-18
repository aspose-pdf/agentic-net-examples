using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AddMultiLineTextStampExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample PDF document
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the created PDF
            using (Document doc = new Document("input.pdf"))
            {
                // Prepare multi‑line disclaimer text. Empty lines are added to increase line spacing.
                string disclaimerText = "DISCLAIMER:\n\nThis document is confidential.\n\nDo not distribute without permission.\n\nThank you.";

                // Create a TextStamp with the disclaimer text
                TextStamp textStamp = new TextStamp(disclaimerText);

                // Configure the visual appearance of the stamp
                textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                textStamp.TextState.FontSize = 12;
                textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                textStamp.HorizontalAlignment = HorizontalAlignment.Center;
                textStamp.VerticalAlignment = VerticalAlignment.Bottom;
                textStamp.BottomMargin = 20; // distance from the bottom edge

                // Add the stamp to each page of the document
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