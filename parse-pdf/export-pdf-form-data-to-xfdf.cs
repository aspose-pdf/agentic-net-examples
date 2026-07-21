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
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
        {
            // The core API does not expose a direct ExportFdf method for form fields.
            // As an alternative, export annotations (which include form field data) to XFDF.
            // This demonstrates how to write the data to a stream without using Facades.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                doc.ExportAnnotationsToXfdf(fdfStream);
            }
        }

        Console.WriteLine($"Export completed to '{fdfPath}'.");
    }
}