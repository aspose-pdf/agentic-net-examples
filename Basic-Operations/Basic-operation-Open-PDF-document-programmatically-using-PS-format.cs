using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPs = "input.ps";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPs))
        {
            Console.Error.WriteLine($"File not found: {inputPs}");
            return;
        }

        try
        {
            // Load the PostScript file using PsLoadOptions.
            // This creates a PDF Document instance from the PS input.
            using (Document doc = new Document(inputPs, new PsLoadOptions()))
            {
                // Perform any desired operations on the PDF document here.

                // Save the document as a PDF file.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PostScript file converted and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}