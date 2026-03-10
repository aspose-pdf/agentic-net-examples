using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with AcroForm
        const string outputPdfPath = "output.pdf";  // destination PDF after import
        const string xfdfFilePath  = "data.xfdf";   // XFDF file containing field values

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xfdfFilePath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfFilePath}");
            return;
        }

        // Open the XFDF stream for reading
        using (FileStream xfdfStream = new FileStream(xfdfFilePath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the Form facade and bind the source PDF
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);          // load the PDF document
                form.ImportXfdf(xfdfStream);        // import field values from XFDF
                form.Save(outputPdfPath);            // save the updated PDF
            }
        }

        Console.WriteLine($"PDF with imported XFDF saved to '{outputPdfPath}'.");
    }
}