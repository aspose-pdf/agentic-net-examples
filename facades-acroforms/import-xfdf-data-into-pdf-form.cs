using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // Source PDF with form fields
        const string xfdfPath = "data.xfdf";     // XFDF file containing field values
        const string outputPath = "output.pdf";  // PDF after importing XFDF data

        // Verify input files exist
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

        // Initialize the Form facade with source and destination PDFs
        using (Form form = new Form(pdfPath, outputPath))
        {
            // Open the XFDF stream and import field values
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportXfdf(xfdfStream);
            }

            // Save the PDF containing the imported form data
            form.Save();
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPath}'.");
    }
}