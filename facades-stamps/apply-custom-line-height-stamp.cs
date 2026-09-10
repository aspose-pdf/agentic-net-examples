using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create formatted text with custom line spacing (extra spacing after each line)
        FormattedText formattedText = new FormattedText();
        formattedText.AddNewLineText("CONFIDENTIAL", 5f);          // 5 points extra spacing
        formattedText.AddNewLineText("DO NOT DISTRIBUTE", 5f);
        formattedText.AddNewLineText("FOR INTERNAL USE ONLY", 5f);

        // Create a stamp and bind the formatted text to it
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formattedText);

        // Position the stamp (XIndent, YIndent) and set appearance
        stamp.SetOrigin(100, 400);   // place near the center of the page
        stamp.Opacity = 0.3f;        // semi‑transparent
        stamp.IsBackground = true;  // draw behind page content

        // Apply the stamp to all pages using PdfFileStamp facade
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            fileStamp.BindPdf(inputPath);   // load source PDF
            fileStamp.AddStamp(stamp);      // add the prepared stamp
            fileStamp.Save(outputPath);     // save the result
            fileStamp.Close();              // close resources
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}