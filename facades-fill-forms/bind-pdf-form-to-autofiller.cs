using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF form
        const string sourcePdfPath = "input_form.pdf";

        // Create an AutoFiller instance and bind it to the PDF form
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF file – this initializes the facade with the document
            autoFiller.BindPdf(sourcePdfPath);

            // At this point the AutoFiller is ready for further operations
            // (e.g., importing data, saving the filled PDF, etc.).
            // For demonstration we simply close the facade after binding.
        }

        Console.WriteLine("AutoFiller bound to PDF form successfully.");
    }
}