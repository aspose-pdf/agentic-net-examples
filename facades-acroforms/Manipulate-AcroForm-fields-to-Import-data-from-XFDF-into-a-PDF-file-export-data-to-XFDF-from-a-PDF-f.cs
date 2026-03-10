using System;
using System.IO;
using Aspose.Pdf.Facades;

class AcroFormXfdfExample
{
    // Imports field values from an XFDF file into a PDF form and saves the result.
    static void ImportXfdf(string pdfInputPath, string xfdfInputPath, string pdfOutputPath)
    {
        // Ensure input files exist.
        if (!File.Exists(pdfInputPath))
            throw new FileNotFoundException($"PDF file not found: {pdfInputPath}");
        if (!File.Exists(xfdfInputPath))
            throw new FileNotFoundException($"XFDF file not found: {xfdfInputPath}");

        // Create a Form facade bound to the source PDF.
        using (Form form = new Form(pdfInputPath))
        {
            // Open the XFDF stream and import its field values.
            using (FileStream xfdfStream = File.OpenRead(xfdfInputPath))
            {
                form.ImportXfdf(xfdfStream);
            }

            // Save the modified PDF to the specified output path.
            form.Save(pdfOutputPath);
        }
    }

    // Exports field values from a PDF form into an XFDF file.
    static void ExportXfdf(string pdfInputPath, string xfdfOutputPath)
    {
        if (!File.Exists(pdfInputPath))
            throw new FileNotFoundException($"PDF file not found: {pdfInputPath}");

        // Create a Form facade bound to the source PDF.
        using (Form form = new Form(pdfInputPath))
        {
            // Open the output XFDF stream and write the exported data.
            using (FileStream xfdfStream = new FileStream(xfdfOutputPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }
        }
    }

    static void Main()
    {
        // Paths for demonstration.
        const string sourcePdf = "form_template.pdf";
        const string sourceXfdf = "data_to_import.xfdf";
        const string filledPdf = "form_filled.pdf";
        const string exportedXfdf = "exported_data.xfdf";

        try
        {
            // Import XFDF data into PDF.
            ImportXfdf(sourcePdf, sourceXfdf, filledPdf);
            Console.WriteLine($"Imported XFDF into PDF: {filledPdf}");

            // Export XFDF data from the newly created PDF.
            ExportXfdf(filledPdf, exportedXfdf);
            Console.WriteLine($"Exported XFDF from PDF: {exportedXfdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}