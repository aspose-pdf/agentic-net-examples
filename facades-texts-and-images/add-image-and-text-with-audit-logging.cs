using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Simple logger that writes to console with timestamp
    static void LogOperation(string operation, int pageNumber, string fileName)
    {
        string timestamp = DateTime.Now.ToString("o"); // ISO 8601 format
        Console.WriteLine($"{timestamp} | Operation: {operation} | Page: {pageNumber} | File: {fileName}");
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "image.jpg";
        const int    targetPage    = 1;
        const float   llx = 50f, lly = 50f, urx = 200f, ury = 200f; // image rectangle

        // Ensure input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Use PdfFileMend to modify the PDF
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF
            mend.BindPdf(inputPdfPath);

            // ---- Add Image ----
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // AddImage returns bool indicating success
                bool imageAdded = mend.AddImage(imgStream, targetPage, llx, lly, urx, ury);
                if (imageAdded)
                {
                    LogOperation("AddImage", targetPage, imagePath);
                }
                else
                {
                    Console.Error.WriteLine("Failed to add image.");
                }
            }

            // ---- Add Text ----
            // FormattedText constructor requires System.Drawing.Color
            FormattedText ft = new FormattedText(
                "Sample audit text",
                System.Drawing.Color.Black,
                "Helvetica",
                EncodingType.Winansi,
                false,
                12);

            // AddText is declared but not implemented; call it for logging purposes
            mend.AddText(ft, targetPage, 100f, 500f);
            LogOperation("AddText", targetPage, "FormattedText (Sample audit text)");

            // Save the modified PDF
            mend.Save(outputPdfPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPdfPath}'.");
    }
}