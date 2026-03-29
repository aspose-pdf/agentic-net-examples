using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imageFile = "image.jpg";
        const string intermediatePdf = "intermediate.pdf";
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

        // Step 1: Attach the image file to the PDF (no visual annotation).
        Aspose.Pdf.Facades.PdfContentEditor attachmentEditor = new Aspose.Pdf.Facades.PdfContentEditor();
        attachmentEditor.BindPdf(inputPdf);
        attachmentEditor.AddDocumentAttachment(imageFile, "Embedded image attachment");
        attachmentEditor.Save(intermediatePdf);

        // Step 2: Add a stamp that displays the same image.
        Aspose.Pdf.Facades.PdfFileStamp fileStamp = new Aspose.Pdf.Facades.PdfFileStamp(intermediatePdf, outputPdf);
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindImage(imageFile);
        stamp.SetOrigin(100, 500);
        stamp.SetImageSize(200f, 200f);
        stamp.Opacity = 0.8f;
        stamp.IsBackground = true;
        fileStamp.AddStamp(stamp);
        fileStamp.Close();

        Console.WriteLine($"Output PDF with attachment and stamp saved to '{outputPdf}'.");
    }
}
