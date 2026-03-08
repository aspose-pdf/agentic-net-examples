using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string newImage = "newImage.jpg";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(newImage))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImage}");
            return;
        }

        // Initialize the PDF content editor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the existing PDF document
        editor.BindPdf(inputPdf);

        // Replace the first image (index 1) on the first page (pageNumber 1) with the new image file
        editor.ReplaceImage(pageNumber: 1, index: 1, imageFile: newImage);

        // Save the modified PDF to the output path
        editor.Save(outputPdf);

        // Close the editor to release resources
        editor.Close();

        Console.WriteLine($"Image replaced successfully. Output saved to '{outputPdf}'.");
    }
}