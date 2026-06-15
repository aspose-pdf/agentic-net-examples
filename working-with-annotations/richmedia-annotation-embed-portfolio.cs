using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

public class RichMediaPortfolioExample
{
    public static void Main()
    {
        // Step 1: Create a simple PDF to work with
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Step 2: Create a PDF that will be embedded as a portfolio
        using (Document portfolio = new Document())
        {
            portfolio.Pages.Add();
            TextFragment tf = new TextFragment("This is the embedded portfolio PDF.");
            portfolio.Pages[1].Paragraphs.Add(tf);
            portfolio.Save("portfolio.pdf");
        }

        // Step 3: Open the original PDF and add RichMediaAnnotation
        using (Document doc = new Document("input.pdf"))
        {
            // Define annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create RichMediaAnnotation on the first page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect);

            // Embed the portfolio PDF as the content of the annotation
            using (FileStream fs = new FileStream("portfolio.pdf", FileMode.Open, FileAccess.Read))
            {
                richMedia.SetContent("application/pdf", fs);
            }

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(richMedia);

            // Disable printing by applying encryption with permissions that exclude PrintDocument
            Permissions perms = Permissions.ExtractContent; // allow content extraction but not printing
            doc.Encrypt("", "", perms, CryptoAlgorithm.AESx128);

            // Save the final PDF
            doc.Save("output.pdf");
        }
    }
}
