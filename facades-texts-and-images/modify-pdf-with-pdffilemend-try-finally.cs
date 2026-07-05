using System;
using System.IO;
using Aspose.Pdf;
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

        PdfFileMend mend = null;
        try
        {
            // Initialize the facade and bind the source PDF
            mend = new PdfFileMend();
            mend.BindPdf(inputPath);

            // Perform any desired modifications here (e.g., AddText, AddImage, etc.)

            // Save the changes to the output file
            mend.Save(outputPath);
        }
        finally
        {
            // Ensure the facade is closed to release resources and finalize the PDF
            if (mend != null)
            {
                mend.Close();
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}