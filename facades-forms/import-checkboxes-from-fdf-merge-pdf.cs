using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "source.pdf";
        const string outputPdf = "merged.pdf";
        const string fdfFile   = "checkboxes.fdf";

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(fdfFile))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfFile}");
            return;
        }

        // Initialize the Form facade with the source PDF and the target PDF.
        using (Form form = new Form(sourcePdf, outputPdf))
        {
            // Import checkbox definitions (and their values) from the FDF stream.
            using (FileStream fdfStream = new FileStream(fdfFile, FileMode.Open, FileAccess.Read))
            {
                form.ImportFdf(fdfStream);
            }

            // Save the PDF. Existing fields are kept; new fields from the FDF are added only if they do not already exist.
            form.Save();
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}