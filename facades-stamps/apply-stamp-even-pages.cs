using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document to obtain the total page count
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            List<int> evenPages = new List<int>();
            for (int i = 1; i <= pageCount; i++)
            {
                if (i % 2 == 0)
                {
                    evenPages.Add(i);
                }
            }

            // Create a simple text stamp (use System.Drawing.Color for the color argument)
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                "Even Page Aspose.Pdf.Facades.Stamp",
                System.Drawing.Color.Red,
                "Helvetica",
                Aspose.Pdf.Facades.EncodingType.Winansi,
                false,
                36f);

            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindLogo(ft);
            stamp.IsBackground = true;
            stamp.Pages = evenPages.ToArray();

            // Apply the stamp only to the even pages
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(inputPath);
                fileStamp.AddStamp(stamp);
                fileStamp.Save(outputPath);
                fileStamp.Close();
            }

            Console.WriteLine($"Aspose.Pdf.Facades.Stamp applied to even pages. Output saved to '{outputPath}'.");
        }
    }
}
