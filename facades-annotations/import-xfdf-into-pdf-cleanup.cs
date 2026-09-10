using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfTemp = "temp.xfdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xfdfTemp))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfTemp}");
            return;
        }

        try
        {
            // Bind source PDF and specify output PDF using Form facade
            using (Form form = new Form(inputPdf, outputPdf))
            {
                // Open XFDF file as a stream and import its data
                using (FileStream xfdfStream = new FileStream(xfdfTemp, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXfdf(xfdfStream);
                }

                // Save the PDF with imported XFDF data
                form.Save();
            }

            // Delete the temporary XFDF file after successful import and save
            File.Delete(xfdfTemp);
            Console.WriteLine("Import completed and temporary XFDF file deleted.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}