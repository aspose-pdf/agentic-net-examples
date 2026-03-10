using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string fdfFile = "data.fdf";
        const string outputPdf = "output.pdf";

        // Export AcroForm fields from a PDF to an FDF file
        ExportFieldsToFdf(inputPdf, fdfFile);

        // Import AcroForm fields from an FDF file into a new PDF
        ImportFieldsFromFdf(inputPdf, fdfFile, outputPdf);
    }

    static void ExportFieldsToFdf(string pdfPath, string fdfPath)
    {
        // Load the PDF using the Form facade
        using (Form form = new Form(pdfPath))
        {
            // Create a writable stream for the FDF output
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportFdf(fdfStream);
            }
        }
    }

    static void ImportFieldsFromFdf(string sourcePdfPath, string fdfPath, string resultPdfPath)
    {
        // Initialize the Form facade with source PDF and target PDF file name
        using (Form form = new Form(sourcePdfPath, resultPdfPath))
        {
            // Open the FDF file for reading
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Persist the changes to the new PDF
            form.Save();
        }
    }
}