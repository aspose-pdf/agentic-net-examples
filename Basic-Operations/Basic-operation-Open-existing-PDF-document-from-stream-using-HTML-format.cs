using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input HTML file (can be any HTML source)
        const string htmlFilePath = "input.html";
        // Output PDF file
        const string pdfOutputPath = "output.pdf";

        // Verify the HTML source exists
        if (!File.Exists(htmlFilePath))
        {
            Console.Error.WriteLine($"Error: HTML file not found at '{htmlFilePath}'.");
            return;
        }

        try
        {
            // Open the HTML file as a stream
            using (FileStream htmlStream = new FileStream(htmlFilePath, FileMode.Open, FileAccess.Read))
            {
                // Load options specifying that the input format is HTML
                HtmlLoadOptions loadOptions = new HtmlLoadOptions();

                // Create a Document from the HTML stream using the load options
                using (Document pdfDocument = new Document(htmlStream, loadOptions))
                {
                    // Save the resulting PDF to the specified path
                    pdfDocument.Save(pdfOutputPath);
                }
            }

            Console.WriteLine($"PDF successfully created at '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}