using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_attachments.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the content editor facade, bind the PDF, delete all attachments, and save the result
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);          // Load the PDF file
        editor.DeleteAttachments();         // Remove every embedded attachment
        editor.Save(outputPath);            // Persist the modified PDF
        editor.Close();                     // Release resources held by the facade

        Console.WriteLine($"Attachments removed. Output saved to '{outputPath}'.");
    }
}