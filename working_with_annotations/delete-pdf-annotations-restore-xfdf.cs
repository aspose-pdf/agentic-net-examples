using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "backup.xfdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Remove all annotations from every page
                foreach (Page page in doc.Pages)
                {
                    page.Annotations.Delete(); // deletes all annotations on the page
                }

                // Re‑import annotations from the XFDF backup file
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations restored and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}