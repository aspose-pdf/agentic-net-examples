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

        // Initialize PdfFileInfo with the source PDF
        Aspose.Pdf.Facades.PdfFileInfo pdfInfo = new Aspose.Pdf.Facades.PdfFileInfo(inputPath);

        // Set the document language via the Header dictionary (Lang entry)
        pdfInfo.Header["Lang"] = "en-US";

        // Save the updated PDF to a new file
        bool saved = pdfInfo.SaveNewInfo(outputPath);
        if (saved)
        {
            Console.WriteLine($"Language set to en-US and saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to save the updated PDF.");
        }

        // Release resources
        pdfInfo.Close();
    }
}