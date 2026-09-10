using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Rectangle is required for the CreateWebLink method

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_link.pdf"; // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (facade) inside a using block for deterministic disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Define the clickable rectangle on page 2 (x, y, width, height)
            // Adjust the coordinates as needed for your document
            Rectangle linkRect = new Rectangle(100, 500, 200, 50);

            // Create a web link that opens the specified URL when clicked
            // Overload without color avoids System.Drawing.Color usage
            editor.CreateWebLink(linkRect, "https://example.com", 2);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Hyperlink added and saved to '{outputPdf}'.");
    }
}