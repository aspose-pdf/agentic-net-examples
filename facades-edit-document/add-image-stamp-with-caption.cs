using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // for Rectangle used by PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.jpg";
        const string caption   = "Sample Caption";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ---------- Add image as a stamp annotation ----------
        // Bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf);

        // Create a stamp that holds the image
        Stamp imgStamp = new Stamp();
        imgStamp.BindImage(imagePath);               // set the image to be used as stamp
        imgStamp.SetOrigin(50, 150);                 // lower‑left corner of the stamp (x=50, y=150)
        imgStamp.SetImageSize(100, 100);             // optional: define width and height of the image

        // Add the stamp to the document and save
        fileStamp.AddStamp(imgStamp);
        fileStamp.Save(outputPdf);
        fileStamp.Close();

        // ---------- Add caption underneath the image ----------
        // Use PdfContentEditor to create a free‑text annotation
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(outputPdf);

        // Define a rectangle for the caption (positioned below the image)
        // Adjust the Y coordinate as needed; here we place it at y = 130
        Rectangle captionRect = new Rectangle(50, 130, 200, 150);
        editor.CreateFreeText(captionRect, caption, 1); // page numbers are 1‑based

        // Save the final document
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"Image annotation with caption saved to '{outputPdf}'.");
    }
}