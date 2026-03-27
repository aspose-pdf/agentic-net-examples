using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string blankPdf = "blank.pdf";
        const string outputPdf = "merged.pdf";

        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        using (Document blankDoc = new Document())
        {
            blankDoc.Pages.Add();
            blankDoc.Save(blankPdf);
        }

        string[] inputFiles = new string[] { firstPdf, blankPdf, secondPdf };

        PdfFileEditor fileEditor = new PdfFileEditor();
        bool success = fileEditor.Concatenate(inputFiles, outputPdf);
        if (success)
        {
            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to concatenate PDFs.");
        }

        try
        {
            File.Delete(blankPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}