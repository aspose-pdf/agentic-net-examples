using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF document
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the PDF and add a text stamp to the first page
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Define the text appearance: custom font, size, and blue color
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont("Arial");
            textState.FontSize = 24;
            textState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create the text stamp with the defined appearance
            TextStamp textStamp = new TextStamp("Confidential", textState);
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Center;

            // Add the stamp to the first page (page indexing is 1‑based)
            pdfDoc.Pages[1].AddStamp(textStamp);

            // Save the modified PDF
            pdfDoc.Save("output.pdf");
        }
    }
}