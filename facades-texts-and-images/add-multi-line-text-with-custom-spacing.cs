using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a formatted text block with multiple lines.
        // The constructor requires System.Drawing.Color for the text color.
        FormattedText formattedText = new FormattedText(
            "First line of text",                     // initial line
            System.Drawing.Color.Black,               // text color
            "Helvetica",                              // font name
            EncodingType.Winansi,                     // encoding
            false,                                    // embed font flag
            12);                                      // font size

        // Add additional lines with custom line spacing (e.g., 6 points after each line).
        formattedText.AddNewLineText("Second line with custom spacing", 6);
        formattedText.AddNewLineText("Third line with custom spacing", 6);

        // Use the PdfFileMend facade (Aspose.Pdf.Facades) to add the text to page 3.
        // lowerLeftX = 0 positions the text at the left edge.
        // lowerLeftY = 0 positions the text at the bottom; adjust as needed.
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPath);
        mend.AddText(formattedText, 3, 0, 0); // page number is 1‑based.
        mend.Save(outputPath);
        mend.Close();

        Console.WriteLine($"Multi‑line text added to page 3 and saved as '{outputPath}'.");
    }
}