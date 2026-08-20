using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, XFDF data file and the output PDF
        const string pdfPath   = "input_form.pdf";
        const string xfdfPath  = "data.xfdf";
        const string outputPdf = "filled_form.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Use the Form facade to import XFDF data into the PDF
        // The Form constructor takes the source PDF and the destination PDF file name
        using (Form form = new Form(pdfPath, outputPdf))
        {
            // Open the XFDF file as a read‑only stream
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                // Import the field values from the XFDF stream
                form.ImportXfdf(xfdfStream);
            }

            // Persist the changes to the output PDF
            form.Save();
        }

        Console.WriteLine($"Form fields imported successfully. Output saved to '{outputPdf}'.");
    }
}