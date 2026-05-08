using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Simple logger that writes timestamp, operation, file name and page number
    static void Log(string operation, string fileName, int pageNumber)
    {
        Console.WriteLine($"{DateTime.Now:O} | {operation} | File: {fileName} | Page: {pageNumber}");
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Example resources
        const string imagePath = "logo.png";
        const string text      = "Sample Text";

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

        // Initialize the PdfFileMend facade
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // ---------- AddImage ----------
        int imagePage = 1;
        float imgLlx = 50f, imgLly = 50f, imgUrx = 200f, imgUry = 200f;
        bool imageAdded = mend.AddImage(imagePath, imagePage, imgLlx, imgLly, imgUrx, imgUry);
        if (imageAdded)
        {
            Log("AddImage", imagePath, imagePage);
        }

        // ---------- AddText ----------
        int textPage = 2;
        // FormattedText constructor requires System.Drawing.Color
        FormattedText formatted = new FormattedText(
            text,
            System.Drawing.Color.Black,
            "Helvetica",
            EncodingType.Winansi,
            false,
            12);

        float txtLlx = 100f, txtLly = 500f, txtUrx = 300f, txtUry = 520f;
        bool textAdded = mend.AddText(formatted, textPage, txtLlx, txtLly, txtUrx, txtUry);
        if (textAdded)
        {
            Log("AddText", "FormattedText", textPage);
        }

        // Save the modified PDF and release resources
        mend.Save(outputPdf);
        mend.Close();

        Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
    }
}