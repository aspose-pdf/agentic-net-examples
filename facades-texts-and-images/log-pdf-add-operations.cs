using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPdf = "output.pdf";
        string imageFile = "image.jpg";
        string textContent = "Sample audit text";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }
        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine("Image file not found: " + imageFile);
            return;
        }

        using (Document document = new Document(inputPdf))
        {
            // Fully qualify Facades types to avoid ambiguity
            Aspose.Pdf.Facades.PdfFileMend mend = new Aspose.Pdf.Facades.PdfFileMend();
            mend.BindPdf(document);

            // Add image to page 1
            int pageNumber = 1;
            float llx = 100.0f;
            float lly = 500.0f;
            float urx = 300.0f;
            float ury = 700.0f;
            mend.AddImage(imageFile, pageNumber, llx, lly, urx, ury);
            Console.WriteLine($"{DateTime.Now:O} - AddImage - Page {pageNumber} - File {Path.GetFileName(imageFile)}");

            // Add text to page 1 (audit log)
            Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText(
                textContent,
                System.Drawing.Color.Black,               // use System.Drawing.Color as required by Facades
                "Helvetica",
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,
                12f);                                      // font size as float
            float textX = 120.0f;
            float textY = 480.0f;
            mend.AddText(formattedText, pageNumber, textX, textY);
            Console.WriteLine($"{DateTime.Now:O} - AddText - Page {pageNumber} - Text \"{textContent}\"");

            // Save the modified PDF
            mend.Save(outputPdf);
            mend.Close();
        }

        Console.WriteLine("PDF processing completed.");
    }
}
