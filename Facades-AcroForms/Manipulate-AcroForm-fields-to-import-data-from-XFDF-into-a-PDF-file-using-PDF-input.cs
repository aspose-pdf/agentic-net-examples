using System;
using System.IO;
using Aspose.Pdf.Facades;

class ImportXfdfToPdf
{
    static void Main()
    {
        // Input PDF, XFDF and output PDF file paths
        const string pdfPath = "input.pdf";
        const string xfdfPath = "data.xfdf";
        const string outputPath = "output.pdf";

        // Verify that the source files exist
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

        try
        {
            // Create the Form facade and bind it to the existing PDF document
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath); // load PDF

                // Open the XFDF file as a stream and import its field values
                using (FileStream xfdfStream = File.OpenRead(xfdfPath))
                {
                    form.ImportXfdf(xfdfStream);
                }

                // Save the updated PDF with imported data
                form.Save(outputPath); // {DocumentVar}.Save({OutputPath});
            }

            Console.WriteLine($"XFDF data imported successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}