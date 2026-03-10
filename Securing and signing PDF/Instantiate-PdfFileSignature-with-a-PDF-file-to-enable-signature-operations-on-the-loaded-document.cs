using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF file that will be signed
        const string pdfPath = "document.pdf";

        // Ensure the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Create an instance of PdfFileSignature – this object enables
        // signature operations (e.g., adding a digital signature) on the PDF.
        PdfFileSignature pdfSignature = new PdfFileSignature();

        // Bind the PDF file to the PdfFileSignature instance.
        // After binding, the instance is ready for further signature actions.
        pdfSignature.BindPdf(pdfPath);

        // At this point you can configure signature properties and call
        // pdfSignature.Sign(...). For this example we only demonstrate
        // instantiation and binding.
        Console.WriteLine("PdfFileSignature instantiated and PDF bound successfully.");
    }
}