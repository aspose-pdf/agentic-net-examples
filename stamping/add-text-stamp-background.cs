using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with three pages
        string inputFile = "input.pdf";
        using (Document sampleDoc = new Document())
        {
            for (int i = 0; i < 3; i++)
            {
                sampleDoc.Pages.Add();
            }
            sampleDoc.Save(inputFile);
        }

        // Step 2: Open the PDF and add a text stamp with a background box
        using (Document doc = new Document(inputFile))
        {
            // Create a text stamp
            TextStamp textStamp = new TextStamp("CONFIDENTIAL");
            textStamp.Background = true; // place behind page content
            textStamp.Opacity = 0.5f;
            textStamp.XIndent = 100;
            textStamp.YIndent = 500;
            textStamp.Width = 200;
            textStamp.Height = 50;

            // Configure text appearance
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.ForegroundColor = Color.White;
            textStamp.TextState.BackgroundColor = Color.Black;

            // Add the stamp to each page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            doc.Save("output.pdf");
        }
    }
}
