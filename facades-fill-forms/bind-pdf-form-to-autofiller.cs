using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF form
        const string sourcePdfPath = "form.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {sourcePdfPath}");
            return;
        }

        // Create an AutoFiller instance and bind it to the PDF form
        // AutoFiller implements IDisposable, so we use a using block for deterministic cleanup
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the PDF file to the AutoFiller
            autoFiller.BindPdf(sourcePdfPath);

            // At this point the AutoFiller is ready for further operations
            // (e.g., importing data, filling fields, saving the result).
            // Example placeholder:
            // autoFiller.ImportDataTable(myDataTable);
            // autoFiller.Save("filled_form.pdf");
        }

        Console.WriteLine("AutoFiller successfully bound to the PDF form.");
    }
}