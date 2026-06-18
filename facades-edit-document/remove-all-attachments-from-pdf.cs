using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class RemoveAttachmentsExample
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfContentEditor implements IDisposable via SaveableFacade, so use a using block.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF file.
                editor.BindPdf(inputPath);

                // Delete all embedded attachments.
                editor.DeleteAttachments();

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Attachments removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}