using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // PDF files that will be used as individual stamps (first page of each)
        string[] stampFiles = new string[] { "stamp1.pdf", "stamp2.pdf", "stamp3.pdf" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        foreach (string stampPath in stampFiles)
        {
            if (!File.Exists(stampPath))
            {
                Console.Error.WriteLine($"Aspose.Pdf.Facades.Stamp PDF not found: {stampPath}");
                return;
            }
        }

        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the document that will receive the stamps
            fileStamp.BindPdf(inputPdf);

            // Add each stamp PDF (using its first page) to the stamp collection
            foreach (string stampPath in stampFiles)
            {
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindPdf(stampPath, 1); // use first page of the stamp PDF
                stamp.IsBackground = true;   // place stamp behind existing content
                fileStamp.AddStamp(stamp);
            }

            // Save the resulting PDF
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}