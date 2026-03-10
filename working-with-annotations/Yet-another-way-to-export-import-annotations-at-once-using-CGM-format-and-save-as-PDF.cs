using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";      // CGM source file
        const string xfdfPath  = "annotations.xfdf"; // Temporary XFDF file
        const string pdfPath   = "output.pdf";     // Resulting PDF file

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the CGM file into a PDF document using CgmLoadOptions
            using (Document doc = new Document(cgmPath, new CgmLoadOptions()))
            {
                // Export any existing annotations to an XFDF file
                doc.ExportAnnotationsToXfdf(xfdfPath);

                // Import the annotations back from the XFDF file
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the final document as PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"PDF successfully saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}