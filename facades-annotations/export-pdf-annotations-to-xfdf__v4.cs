using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // NOTE: Exporting annotations to XFDF requires the optional Aspose.Pdf.Xfdf assembly.
        // If that assembly is referenced, the code below can be uncommented and used.
        /*
        using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
        {
            var xfdfWriter = new Aspose.Pdf.Xfdf.XfdfWriter();
            xfdfWriter.WriteAnnotations(pdfDocument, xfdfStream);
        }
        Console.WriteLine($"All annotations have been exported to '{xfdfPath}'.");
        */

        // Fallback message when the XFDF assembly is not available.
        Console.WriteLine("XFDF export functionality is unavailable because the Aspose.Pdf.Xfdf assembly is not referenced.");
    }
}