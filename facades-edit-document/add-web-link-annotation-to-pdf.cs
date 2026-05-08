using System;
using System.IO;
using System.Drawing;               // Rectangle, Color
using Aspose.Pdf.Facades;          // PdfContentEditor

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the target website URL
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string websiteUrl = "https://www.example.com";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a PdfContentEditor facade, bind the source PDF,
        // add a web link annotation, and save the result.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the existing PDF document
            editor.BindPdf(inputPdf);

            // Define the clickable rectangle (x, y, width, height)
            // Coordinates are in points; adjust as needed.
            Rectangle linkRect = new Rectangle(100, 500, 200, 50);

            // Page number is 1‑based. The link will be placed on page 1.
            int pageNumber = 1;

            // Create the web link annotation that opens the external URL.
            // Using the overload without color for simplicity.
            editor.CreateWebLink(linkRect, websiteUrl, pageNumber);

            // Persist the changes to a new PDF file.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Web link added and saved to '{outputPdf}'.");
    }
}