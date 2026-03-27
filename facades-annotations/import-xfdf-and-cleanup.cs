using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfTempPath = "temp.xfdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xfdfTempPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfTempPath}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Import annotations from the XFDF file
                pdfDoc.ImportAnnotationsFromXfdf(xfdfTempPath);
                // Save the updated PDF
                pdfDoc.Save(outputPdfPath);
            }

            // Delete the temporary XFDF file after successful import and save
            File.Delete(xfdfTempPath);
            Console.WriteLine($"Temporary XFDF file deleted: {xfdfTempPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
