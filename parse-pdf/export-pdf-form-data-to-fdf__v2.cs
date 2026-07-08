using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "output.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Initialize the Form facade with the loaded document
            Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form(doc);

            // Export the form fields to an FDF file
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                formFacade.ExportFdf(fdfStream);
                // The using block ensures the stream is closed
            }
        }

        Console.WriteLine($"Form data exported to '{fdfPath}'.");
    }
}