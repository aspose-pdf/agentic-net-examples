using System;
using System.IO;
using System.Drawing;                     // System.Drawing for Color and FontStyle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Helper to log audit information
    static void LogOperation(string operation, int pageNumber, string fileName)
    {
        // ISO 8601 format for consistency
        string timestamp = DateTime.UtcNow.ToString("u");
        Console.WriteLine($"{timestamp} - {operation} on page {pageNumber} in file '{fileName}'");
    }

    static void Main()
    {
        const string inputPdf   = "source.pdf";
        const string outputPdf  = "modified.pdf";
        const string imagePath  = "logo.png";
        const string text       = "Confidential";

        // Validate input files
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

        // PdfFileMend works on an existing PDF; bind it, perform operations, then save.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF
            mend.BindPdf(inputPdf);

            // ---------- Add Image ----------
            // Add the image to page 1 at coordinates (50, 50) - (150, 150)
            int imagePage = 1;
            float llx = 50f, lly = 50f, urx = 150f, ury = 150f;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                bool imageAdded = mend.AddImage(imgStream, imagePage, llx, lly, urx, ury);
                if (imageAdded)
                {
                    LogOperation("AddImage", imagePage, outputPdf);
                }
                else
                {
                    Console.Error.WriteLine("Failed to add image.");
                }
            }

            // ---------- Add Text ----------
            // Add a text stamp to page 2 at position (200, 700)
            int textPage = 2;
            float textX = 200f, textY = 700f;

            // Use the Facades FormattedText constructor with System.Drawing.Color and EncodingType
            Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
                text,
                System.Drawing.Color.Red,                     // text colour
                "Helvetica",                                 // font name
                Aspose.Pdf.Facades.EncodingType.Winansi,      // encoding
                false,                                        // embed font?
                36f);                                         // font size

            // AddText overload: (FormattedText, pageNumber, lowerLeftX, lowerLeftY)
            bool textAdded = mend.AddText(ft, textPage, textX, textY);
            if (textAdded)
            {
                LogOperation("AddText", textPage, outputPdf);
            }
            else
            {
                Console.Error.WriteLine("Failed to add text.");
            }

            // Save the modified PDF
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
    }
}
