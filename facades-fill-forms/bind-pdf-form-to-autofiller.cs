using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "form_template.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create an AutoFiller instance and bind the source PDF form.
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(pdfPath);

            // The AutoFiller is now ready for further operations,
            // such as importing data and saving the filled PDF.
            // Example (uncomment when needed):
            // autoFiller.Save("filled_output.pdf");
        }
    }
}