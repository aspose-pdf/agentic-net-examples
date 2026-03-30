using System;
using System.IO;
using Aspose.Pdf;
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

        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            editor.BindPdf(inputPath);
            // Example operation: remove all attachments from the PDF
            editor.DeleteAttachments();
            editor.Save(outputPath);
            Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Ensure the facade releases all unmanaged resources
            editor.Close();
            editor.Dispose();
        }
    }
}