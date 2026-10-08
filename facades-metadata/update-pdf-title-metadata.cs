using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle = "Updated Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF file information via Facades API
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Update the Title metadata
                pdfInfo.Title = newTitle;

                // Save the PDF with the updated information
                pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Title updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}