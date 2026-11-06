using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "output.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(pdfPath))
        // Create the output FDF file stream.
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            // Use the Form facade to export AcroForm data to FDF.
            Form formFacade = new Form();
            formFacade.BindPdf(doc);
            formFacade.ExportFdf(fdfStream);
        }

        Console.WriteLine($"Form data exported to FDF file: {fdfPath}");
    }
}
