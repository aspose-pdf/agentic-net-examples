using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string templatePdf = "template.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine("Template PDF not found: " + templatePdf);
            return;
        }

        // Bind the source PDF to the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp from the external template PDF (first page of the template)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindPdf(templatePdf, 1);
        stamp.IsBackground = true;

        // Add the stamp to the document
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        Console.WriteLine("Stamped PDF saved to '" + outputPdf + "'.");
    }
}
