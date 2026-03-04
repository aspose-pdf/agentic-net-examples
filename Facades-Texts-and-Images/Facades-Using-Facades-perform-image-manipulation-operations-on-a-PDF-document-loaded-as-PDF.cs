using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output_modified.pdf";
        const string imageToAdd = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imageToAdd))
        {
            Console.Error.WriteLine($"Image to add not found: {imageToAdd}");
            return;
        }

        // -------------------------------------------------
        // 1. Load the PDF document (using the standard create/load rule)
        // -------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // -------------------------------------------------
            // 2. Add an image to every page using PdfFileMend (Facade)
            // -------------------------------------------------
            PdfFileMend mend = new PdfFileMend(doc);
            // Example coordinates – adjust as needed
            float x = 100f;   // distance from left
            float y = 500f;   // distance from bottom
            float width  = 200f;
            float height = 100f;

            // Add the same image to each page
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                mend.AddImage(imageToAdd, pageNum, x, y, width, height);
            }
            mend.Close(); // close the facade

            // -------------------------------------------------
            // 3. Rotate all pages 90 degrees using PdfPageEditor (Facade)
            // -------------------------------------------------
            PdfPageEditor editor = new PdfPageEditor(doc);
            editor.Rotation = 90;               // rotate 90 degrees
            editor.ApplyChanges();              // apply the rotation
            editor.Close();                     // close the facade

            // -------------------------------------------------
            // 4. Extract all images from the original PDF using PdfExtractor (Facade)
            // -------------------------------------------------
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPdf);         // bind the source PDF
            extractor.ExtractImage();            // prepare image extraction

            int imageIndex = 0;
            while (extractor.HasNextImage())
            {
                string imagePath = $"extracted_image_{imageIndex}.png";
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    // Save each extracted image as PNG
                    extractor.GetNextImage(imgStream, ImageFormat.Png);
                }
                Console.WriteLine($"Extracted image saved to: {imagePath}");
                imageIndex++;
            }
            extractor.Close(); // close the facade

            // -------------------------------------------------
            // 5. Save the modified PDF (using the standard save rule)
            // -------------------------------------------------
            doc.Save(outputPdf);
            Console.WriteLine($"Modified PDF saved to: {outputPdf}");
        }
    }
}