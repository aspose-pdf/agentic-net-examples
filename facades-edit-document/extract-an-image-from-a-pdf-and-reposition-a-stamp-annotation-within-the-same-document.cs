using System;
using System.IO;
using System.Drawing;                     // System.Drawing.Image for extracted stamp image
using Aspose.Pdf.Facades;                // PdfContentEditor, StampInfo

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string extractedImagePath = "extracted_image.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF with PdfContentEditor (facade)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // Retrieve stamps on the first page (page numbers are 1‑based)
            int pageNumber = 1;
            StampInfo[] stamps = editor.GetStamps(pageNumber);

            if (stamps != null && stamps.Length > 0)
            {
                // ----- Extract the image from the first stamp -----
                // StampInfo.Image returns a System.Drawing.Image when the stamp is an image stamp
                Image stampImage = stamps[0].Image;
                if (stampImage != null)
                {
                    stampImage.Save(extractedImagePath);
                    Console.WriteLine($"Extracted image saved to '{extractedImagePath}'.");
                }
                else
                {
                    Console.WriteLine("The first stamp does not contain an image.");
                }

                // ----- Reposition the same stamp -----
                // MoveStamp uses a zero‑based stamp index on the specified page
                double newX = 150.0; // new horizontal position
                double newY = 300.0; // new vertical position
                editor.MoveStamp(pageNumber, 0, newX, newY);
                Console.WriteLine($"Stamp moved to ({newX}, {newY}) on page {pageNumber}.");
            }
            else
            {
                Console.WriteLine("No stamps found on the specified page.");
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}