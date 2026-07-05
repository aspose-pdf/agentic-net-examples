using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string xfdfBackup = "annotations.xfdf";
        const string outputPdf  = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(xfdfBackup))
        {
            Console.Error.WriteLine($"XFDF backup not found: {xfdfBackup}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Remove all existing annotations from every page
                foreach (Page page in doc.Pages)
                {
                    page.Annotations.Delete(); // deletes all annotations in the collection
                }

                // Import annotations from the XFDF backup file
                doc.ImportAnnotationsFromXfdf(xfdfBackup);

                // Save the updated PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Annotations restored and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}