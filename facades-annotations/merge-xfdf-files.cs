using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "merged_output.pdf";

        // List of XFDF files that need to be merged
        string[] xfdfFiles = new string[] { "data1.xfdf", "data2.xfdf", "data3.xfdf" };

        // Validate that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Validate that each XFDF file exists
        foreach (string xfdfPath in xfdfFiles)
        {
            if (!File.Exists(xfdfPath))
            {
                Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
                return;
            }
        }

        try
        {
            // Create a Form object that will read the source PDF and write to the output PDF
            Form form = new Form(inputPdfPath, outputPdfPath);

            // Import each XFDF file sequentially – this merges their field data into the PDF
            foreach (string xfdfPath in xfdfFiles)
            {
                FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read);
                form.ImportXfdf(xfdfStream);
                xfdfStream.Close();
            }

            // Save the resulting PDF with all merged XFDF data
            form.Save();
            Console.WriteLine($"Merged XFDF data saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}