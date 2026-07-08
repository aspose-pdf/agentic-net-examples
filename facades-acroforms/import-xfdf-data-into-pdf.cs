using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with form fields
        const string outputPdfPath = "output.pdf";  // destination PDF after import
        const string xfdfPath      = "data.xfdf";   // XFDF file containing field values

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found – {xfdfPath}");
            return;
        }

        // Create a Form facade specifying source and target PDF files
        Form form = new Form(inputPdfPath, outputPdfPath);

        // Open the XFDF stream and import the field values into the PDF
        using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
        {
            form.ImportXfdf(xfdfStream);
        }

        // Persist the changes to the output PDF
        form.Save();

        Console.WriteLine($"Successfully imported XFDF data and saved to '{outputPdfPath}'.");
    }
}