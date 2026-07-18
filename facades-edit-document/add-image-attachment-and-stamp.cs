using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imageFile = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        // Attach the image file to the PDF
        string tempPdf = Path.GetTempFileName();
        try
        {
            // 1. Add the image as a document attachment
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(inputPdf);
                editor.AddDocumentAttachment(imageFile, "Embedded image attachment");
                editor.Save(tempPdf);
            }

            // 2. Add a stamp that references the same image
            using (PdfFileStamp stampFacade = new PdfFileStamp())
            {
                // Bind the temporary PDF that already contains the attachment
                stampFacade.BindPdf(tempPdf);

                // Create an image stamp (fully qualified to avoid ambiguity)
                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindImage(imageFile);
                stamp.SetOrigin(100, 500);          // X, Y position on the page
                stamp.SetImageSize(150, 150);       // Width, Height of the stamp
                stamp.Opacity = 0.8f;               // Semi‑transparent
                stamp.IsBackground = false;         // Appear on top of page content

                // Apply the stamp to all pages (default behavior)
                stampFacade.AddStamp(stamp);

                // Save the final PDF
                stampFacade.Save(outputPdf);
            }

            Console.WriteLine($"PDF with attachment and stamp saved to '{outputPdf}'.");
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempPdf))
                File.Delete(tempPdf);
        }
    }
}
