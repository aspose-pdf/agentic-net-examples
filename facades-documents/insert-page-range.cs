using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string destinationPdf = "destination.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        if (!File.Exists(destinationPdf))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPdf}");
            return;
        }

        int insertLocation = 1; // position in destination where pages will be inserted
        int startPage = 2;      // first page to take from source
        int endPage = 5;        // last page to take from source

        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.Insert(destinationPdf, insertLocation, sourcePdf, startPage, endPage, outputPdf);
            if (result)
            {
                Console.WriteLine($"Pages {startPage}-{endPage} from '{sourcePdf}' inserted into '{destinationPdf}' at position {insertLocation}. Result saved as '{outputPdf}'.");
            }
            else
            {
                Console.Error.WriteLine("Insertion operation failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
