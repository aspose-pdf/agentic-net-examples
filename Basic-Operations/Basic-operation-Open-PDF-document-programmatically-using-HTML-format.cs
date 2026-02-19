using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Generic input and output file names
        const string htmlFile = "input.html";
        const string pdfFile = "output.pdf";

        // Verify that the HTML source file exists
        if (!File.Exists(htmlFile))
        {
            Console.Error.WriteLine($"Error: HTML file not found – {htmlFile}");
            return;
        }

        try
        {
            // Create load options for HTML conversion
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Load the HTML file into an Aspose.Pdf Document using the load options
            Document pdfDocument = new Document(htmlFile, loadOptions);

            // Save the resulting PDF document to the specified output path
            pdfDocument.Save(pdfFile);

            Console.WriteLine($"HTML file successfully converted and saved as PDF: {pdfFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
