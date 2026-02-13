using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Create a page stamp using the first page of the same document
            // (any page can be used as the stamp source)
            PdfPageStamp pageStamp = new PdfPageStamp(pdfDocument.Pages[1]);

            // Set the new position of the stamp on the target page
            // XIndent and YIndent are measured from the left and bottom edges respectively
            pageStamp.XIndent = 50;   // horizontal offset
            pageStamp.YIndent = 100;  // vertical offset

            // Add the stamp to the first page (or any other page as needed)
            pdfDocument.Pages[1].AddStamp(pageStamp);

            // Save the modified PDF document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Stamp position updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}