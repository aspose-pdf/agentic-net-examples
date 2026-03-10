using System;
using System.IO;
using Aspose.Pdf.Facades;   // Form facade resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing AcroForm fields
        const string outputFdf = "output.fdf";  // Destination FDF file
        const string outputXfdf = "output.xfdf"; // Destination XFDF file

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the PDF file.
        // This loads the document internally; no separate Document object is required.
        Form form = new Form(inputPdf);

        // Export the current values of all form fields to an FDF stream.
        using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
        {
            form.ExportFdf(fdfStream);
        }

        // Export the same field values to an XFDF (XML‑based) stream.
        using (FileStream xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
        {
            form.ExportXfdf(xfdfStream);
        }

        Console.WriteLine($"Form data exported to '{outputFdf}' (FDF) and '{outputXfdf}' (XFDF).");
    }
}