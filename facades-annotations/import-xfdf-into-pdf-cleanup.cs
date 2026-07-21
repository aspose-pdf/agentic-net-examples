using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string xfdfTemp   = "temp.xfdf";
        const string outputPdf  = "output.pdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(xfdfTemp))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfTemp}");
            return;
        }

        try
        {
            // Initialize Form facade with source and destination PDF files
            using (Form form = new Form(sourcePdf, outputPdf))
            {
                // Import XFDF data from temporary file
                using (FileStream xfdfStream = new FileStream(xfdfTemp, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXfdf(xfdfStream);
                }

                // Save the updated PDF
                form.Save();
            }

            // Delete temporary XFDF file after successful import
            File.Delete(xfdfTemp);
            Console.WriteLine("Import completed. Temporary XFDF file removed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}