using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfInputPath   = "input_form.pdf";   // PDF with AcroForm fields
        const string xfdfDataPath   = "data.xfdf";        // XFDF file containing field values
        const string pdfOutputPath  = "filled_form.pdf";  // PDF after importing XFDF
        const string xfdfExportPath = "exported.xfdf";    // XFDF exported from PDF

        // Import XFDF data into a PDF form
        ImportXfdfIntoPdf(pdfInputPath, xfdfDataPath, pdfOutputPath);

        // Export form field data from a PDF to XFDF
        ExportPdfToXfdf(pdfOutputPath, xfdfExportPath);
    }

    // Imports field values from an XFDF file into the specified PDF and saves the result.
    static void ImportXfdfIntoPdf(string pdfPath, string xfdfPath, string outputPdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Form is a SaveableFacade; wrap it in a using block for deterministic disposal.
        using (Form form = new Form(pdfPath))
        {
            // Open the XFDF stream and import its field values into the PDF.
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                form.ImportXfdf(xfdfStream);
            }

            // Save the modified PDF with the imported data.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"XFDF data imported and saved to '{outputPdfPath}'.");
    }

    // Exports all AcroForm field values from a PDF into an XFDF file.
    static void ExportPdfToXfdf(string pdfPath, string outputXfdfPath)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF into a Form facade.
        using (Form form = new Form(pdfPath))
        {
            // Create the output XFDF file.
            using (FileStream xfdfOut = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export field data to the XFDF stream.
                form.ExportXfdf(xfdfOut);
            }
        }

        Console.WriteLine($"Form fields exported to XFDF file '{outputXfdfPath}'.");
    }
}