using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PostScript file and output PDF file paths
        const string psFilePath = "input.ps";
        const string pdfOutputPath = "output.pdf";

        // Verify that the PS file exists
        if (!File.Exists(psFilePath))
        {
            Console.Error.WriteLine($"Error: PostScript file not found at '{psFilePath}'.");
            return;
        }

        try
        {
            // Open the PS file as a stream
            using (FileStream psStream = File.OpenRead(psFilePath))
            {
                // Load options for PS format
                PsLoadOptions loadOptions = new PsLoadOptions();

                // Create a Document from the PS stream using the load options
                Document pdfDocument = new Document(psStream, loadOptions);

                // Save the resulting PDF document
                pdfDocument.Save(pdfOutputPath);
            }

            Console.WriteLine($"PDF successfully created at '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}