using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Prompt the user for the desired page width and height (in points)
        Console.Write("Enter page width (points): ");
        if (!double.TryParse(Console.ReadLine(), out double pageWidth) || pageWidth <= 0)
        {
            Console.Error.WriteLine("Invalid width value.");
            return;
        }

        Console.Write("Enter page height (points): ");
        if (!double.TryParse(Console.ReadLine(), out double pageHeight) || pageHeight <= 0)
        {
            Console.Error.WriteLine("Invalid height value.");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Add a new empty page at the end of the document
                Page newPage = pdfDoc.Pages.Add();

                // Set the custom size for the newly added page
                newPage.SetPageSize(pageWidth, pageHeight);

                // Save the modified document
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Empty page inserted and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}