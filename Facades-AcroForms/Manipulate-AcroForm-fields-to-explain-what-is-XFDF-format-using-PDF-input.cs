using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing an AcroForm
        const string xfdfPath  = "form_data.xfdf"; // XFDF file that will hold field data
        const string outputPdf = "output.pdf";  // PDF after importing XFDF data

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Export the current form field values to XFDF.
        // ------------------------------------------------------------
        // XFDF (XML Forms Data Format) is an XML‑based representation of
        // AcroForm field names and their values.  It can be exchanged
        // between applications and later imported to populate a PDF form.
        using (Form exporter = new Form(inputPdf))
        {
            using (FileStream xfdfOut = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                exporter.ExportXfdf(xfdfOut);
            }
        }

        // ------------------------------------------------------------
        // Import the XFDF data back into a PDF.
        // ------------------------------------------------------------
        // The Form constructor that takes two file names creates a facade
        // bound to the source PDF (first argument) and prepares the target
        // PDF (second argument) for saving.
        using (Form importer = new Form(inputPdf, outputPdf))
        {
            using (FileStream xfdfIn = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                importer.ImportXfdf(xfdfIn);
            }

            // Persist the changes – the form fields are now filled with the
            // values that were stored in the XFDF file.
            importer.Save();
        }

        Console.WriteLine($"XFDF exported to '{xfdfPath}' and imported into '{outputPdf}'.");
    }
}