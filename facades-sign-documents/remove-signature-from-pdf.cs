using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_removed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade and bind the PDF file
            PdfFileSignature pdfSignature = new PdfFileSignature();
            pdfSignature.BindPdf(inputPath);

            // Remove the signature named "ApprovalSignature" using the string overload
            pdfSignature.RemoveSignature("ApprovalSignature");

            // Save the modified PDF
            pdfSignature.Save(outputPath);
            pdfSignature.Close();

            Console.WriteLine($"Signature removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
