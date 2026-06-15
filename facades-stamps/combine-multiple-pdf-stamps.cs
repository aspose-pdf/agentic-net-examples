using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace CombinePdfStampsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a target PDF that will receive the stamps
            using (Document targetDoc = new Document())
            {
                targetDoc.Pages.Add();
                targetDoc.Save("target.pdf");
            }

            // Create first stamp PDF (single page)
            using (Document stampDoc1 = new Document())
            {
                stampDoc1.Pages.Add();
                stampDoc1.Save("stamp1.pdf");
            }

            // Create second stamp PDF (single page)
            using (Document stampDoc2 = new Document())
            {
                stampDoc2.Pages.Add();
                stampDoc2.Save("stamp2.pdf");
            }

            // Initialize PdfFileStamp with input and output files
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.InputFile = "target.pdf";
            fileStamp.OutputFile = "stamped.pdf";

            // First stamp – used as background on all pages
            Aspose.Pdf.Facades.Stamp stamp1 = new Aspose.Pdf.Facades.Stamp();
            stamp1.BindPdf("stamp1.pdf", 1);
            stamp1.IsBackground = true;
            stamp1.Opacity = 0.5f;
            fileStamp.AddStamp(stamp1);

            // Second stamp – placed on top at a specific position
            Aspose.Pdf.Facades.Stamp stamp2 = new Aspose.Pdf.Facades.Stamp();
            stamp2.BindPdf("stamp2.pdf", 1);
            stamp2.IsBackground = false;
            stamp2.Opacity = 0.7f;
            stamp2.SetOrigin(100, 200);
            stamp2.SetImageSize(200, 200);
            fileStamp.AddStamp(stamp2);

            // Apply stamps and save the result
            fileStamp.Close();

            Console.WriteLine("Stamps applied successfully. Output saved to stamped.pdf");
        }
    }
}
