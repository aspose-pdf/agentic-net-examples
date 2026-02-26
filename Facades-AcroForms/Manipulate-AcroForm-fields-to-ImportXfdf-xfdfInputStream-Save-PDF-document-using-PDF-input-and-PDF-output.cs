using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xfdfPath = "data.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // Open the XFDF stream for reading
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                // Initialize the Form facade with source and destination PDF files
                using (Form form = new Form(inputPdfPath, outputPdfPath))
                {
                    // Import field values from the XFDF stream into the PDF form
                    form.ImportXfdf(xfdfStream);

                    // Save the modified PDF to the specified output path
                    form.Save();
                }
            }

            Console.WriteLine($"XFDF data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}