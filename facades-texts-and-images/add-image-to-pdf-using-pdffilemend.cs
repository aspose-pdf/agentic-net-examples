using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // PdfFileMend does not implement IDisposable, so we manage its lifetime manually.
        PdfFileMend mend = new PdfFileMend();

        try
        {
            // Load the source PDF into the Mend object.
            mend.BindPdf(inputPath);

            // -----------------------------------------------------------------
            // Perform PDF modifications here.
            // Example: add a text stamp (uncomment and adjust as needed).
            // TextStamp txtStamp = new TextStamp("Confidential")
            // {
            //     HorizontalAlignment = HorizontalAlignment.Center,
            //     VerticalAlignment   = VerticalAlignment.Middle,
            //     Opacity             = 0.5
            // };
            // mend.AddStamp(txtStamp);
            // -----------------------------------------------------------------

            // Save the modified PDF.
            mend.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released and changes are flushed.
            mend.Close();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}