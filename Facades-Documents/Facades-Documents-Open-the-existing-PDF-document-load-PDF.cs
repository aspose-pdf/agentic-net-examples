using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes
using Aspose.Pdf;          // Document class

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the copy to be saved
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Create the Form facade (represents an AcroForm object)
            Form formFacade = new Form();

            // Load the existing PDF document into the facade
            formFacade.BindPdf(inputPath);

            // The underlying Document object can be accessed via the Document property
            Document pdfDocument = formFacade.Document;

            // Save the document to a new file (uses the document-save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF successfully opened and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}