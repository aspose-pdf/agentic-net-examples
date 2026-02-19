using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PostScript (PS) file to be opened
        string psFilePath = "sample.ps";

        // Verify that the source PS file exists
        if (!File.Exists(psFilePath))
        {
            Console.Error.WriteLine($"Error: PS file not found at '{psFilePath}'.");
            return;
        }

        // Options for loading a PS file into a PDF document
        PsLoadOptions loadOptions = new PsLoadOptions();

        try
        {
            // Load the PS file and create a PDF Document instance
            using (Document pdfDocument = new Document(psFilePath, loadOptions))
            {
                // Define the output PDF file path
                string outputPdfPath = "output.pdf";

                // Save the loaded document as a PDF
                pdfDocument.Save(outputPdfPath);

                Console.WriteLine($"Successfully converted '{psFilePath}' to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during loading or saving
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}