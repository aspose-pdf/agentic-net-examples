using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfFileMend mend = null;
        try
        {
            // Initialize the facade and bind the source PDF
            mend = new PdfFileMend();
            mend.BindPdf(inputPath);

            // Example operation: add an image to page 1 (replace with real parameters as needed)
            // mend.AddImage("logo.png", 1, 100f, 100f, 200f, 200f);

            // Save the modified PDF
            mend.Save(outputPath);
        }
        finally
        {
            // Ensure the facade is closed and resources are released
            mend?.Close();
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}