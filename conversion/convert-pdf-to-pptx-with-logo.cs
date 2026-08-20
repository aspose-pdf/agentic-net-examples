using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string logoImagePath = "logo.png";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // Load the source PDF (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Insert the logo onto each page before conversion
            foreach (Page page in pdfDoc.Pages)
            {
                // Define the rectangle where the logo will be placed (bottom‑right corner)
                double logoWidth = 100;   // desired logo width
                double logoHeight = 50;   // desired logo height
                double margin = 10;       // margin from page edges

                double llx = page.PageInfo.Width - logoWidth - margin; // left
                double lly = margin;                                   // bottom
                double urx = page.PageInfo.Width - margin;             // right
                double ury = lly + logoHeight;                         // top

                // Fully qualified rectangle to avoid ambiguity
                Aspose.Pdf.Rectangle logoRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                // Add the image to the page using a stream (rule: use stream overload)
                using (FileStream imgStream = File.OpenRead(logoImagePath))
                {
                    page.AddImage(imgStream, logoRect);
                }
            }

            // Convert the PDF (now with logos) to PPTX using PptxSaveOptions (rule: explicit save options)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF converted to PPTX with logo on each slide: {outputPptxPath}");
    }
}