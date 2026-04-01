using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string disclaimerPdf = "disclaimer.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(disclaimerPdf))
        {
            Console.Error.WriteLine($"Disclaimer file not found: {disclaimerPdf}");
            return;
        }

        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Load the target PDF document
            fileStamp.BindPdf(inputPdf);

            // Create a stamp from the first page of the disclaimer PDF
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindPdf(disclaimerPdf, 1);
            stamp.IsBackground = true;
            stamp.Opacity = 0.8f;

            // Add the stamp to the document
            fileStamp.AddStamp(stamp);

            // Save the result
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Disclaimer overlay applied. Output saved to '{outputPdf}'.");
    }
}