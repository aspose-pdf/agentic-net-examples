using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "output.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the PDF file
        using (Form form = new Form(pdfPath))
        {
            // Export the form fields to an XFDF file
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }

            // Close the facade (optional, using will dispose it)
            form.Close();
        }

        Console.WriteLine($"Form fields exported to '{xfdfPath}'.");
    }
}